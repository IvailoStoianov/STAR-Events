using Microsoft.AspNetCore.Identity;
using STAREvents.Data.Models;
using STAREvents.Data.Repository.Interfaces;
using STAREvents.Services.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STAREvents.Services.Data
{
    public class EventHelperService : BaseService, IEventHelperService
    {
        private readonly IRepository<Event, object> eventRepository;
        private readonly IRepository<UserEventAttendance, object> attendanceRepository;
        private readonly UserManager<ApplicationUser> userManager;
        public EventHelperService(IRepository<Event, object> _eventRepository,
            IRepository<UserEventAttendance, object> _attendanceRepository,
            UserManager<ApplicationUser> _userManager)
        {
            this.userManager = _userManager;
            this.eventRepository = _eventRepository;
            this.attendanceRepository = _attendanceRepository;
        }
        public async Task JoinEventAsync(Guid eventId, string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            var eventEntity = await eventRepository.GetByIdAsync(eventId);
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

        public async Task LeaveEventAsync(Guid eventId, string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            var eventEntity = await eventRepository.GetByIdAsync(eventId);
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
