using STAREvents.Data.Models;
using STAREvents.Data.Repository.Interfaces;
using STAREvents.Services.Data.Interfaces;
using static STAREvents.Common.ErrorMessagesConstants.EventsServiceErrorMessages;

namespace STAREvents.Services.Data
{
    public class EventHelperService : BaseService, IEventHelperService
    {
        private readonly IRepository<Event, object> eventRepository;
        private readonly IRepository<UserEventAttendance, object> attendanceRepository;
        private readonly IUserAuthService userAuthService;

        public EventHelperService(
            IRepository<Event, object> eventRepository,
            IRepository<UserEventAttendance, object> attendanceRepository,
            IUserAuthService userAuthService)
        {
            this.eventRepository = eventRepository;
            this.attendanceRepository = attendanceRepository;
            this.userAuthService = userAuthService;
        }

        public async Task JoinEventAsync(Guid eventId, Guid userId)
        {
            var user = await userAuthService.GetUserByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new KeyNotFoundException(UserNotFound);
            }

            var eventEntity = await eventRepository.GetByIdAsync(eventId);
            if (eventEntity == null)
            {
                throw new KeyNotFoundException(EventNotFound);
            }

            var attendance = await attendanceRepository
                .FirstOrDefaultAsync(a => a.EventId == eventId && a.UserId == user.Id);
            if (attendance == null)
            {
                var newAttendance = new UserEventAttendance
                {
                    EventId = eventId,
                    UserId = user.Id,
                    JoinedDate = DateTime.UtcNow
                };
                await attendanceRepository.AddAsync(newAttendance);
                eventEntity.NumberOfParticipants++;
                await eventRepository.UpdateAsync(eventEntity);
            }
        }

        public async Task LeaveEventAsync(Guid eventId, Guid userId)
        {
            var user = await userAuthService.GetUserByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new KeyNotFoundException(UserNotFound);
            }

            var eventEntity = await eventRepository.GetByIdAsync(eventId);
            if (eventEntity == null)
            {
                throw new KeyNotFoundException(EventNotFound);
            }

            var attendance = await attendanceRepository.FirstOrDefaultAsync(a => a.EventId == eventId && a.UserId == user.Id);
            if (attendance != null)
            {
                await attendanceRepository.DeleteAsync(attendance);
                eventEntity.NumberOfParticipants--;
                await eventRepository.UpdateAsync(eventEntity);
            }
        }
    }
}
