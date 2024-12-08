using Microsoft.AspNetCore.Identity;
using STAREvents.Data.Models;
using STAREvents.Data.Repository.Interfaces;
using STAREvents.Services.Data.Interfaces;
using System;
using System.Threading.Tasks;

namespace STAREvents.Services.Data
{
    public class EventHelperService : BaseService, IEventHelperService
    {
        private readonly IRepository<Event, object> eventRepository;
        private readonly IRepository<UserEventAttendance, object> attendanceRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public EventHelperService(IRepository<Event, object> eventRepository,
            IRepository<UserEventAttendance, object> attendanceRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.eventRepository = eventRepository;
            this.attendanceRepository = attendanceRepository;
        }

        public async Task JoinEventAsync(Guid eventId, Guid userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            var eventEntity = await eventRepository.GetByIdAsync(eventId);
            if (eventEntity == null)
            {
                throw new KeyNotFoundException("Event not found");
            }

            var attendance = await attendanceRepository.FirstOrDefaultAsync(a => a.EventId == eventId && a.UserId == user.Id);
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
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            var eventEntity = await eventRepository.GetByIdAsync(eventId);
            if (eventEntity == null)
            {
                throw new KeyNotFoundException("Event not found");
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





