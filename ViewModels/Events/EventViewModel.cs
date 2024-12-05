using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using STAREvents.Data.Models;


namespace STAREvents.Web.ViewModels.Events
{
    public class EventViewModel
    {
        public Guid EventId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public DateTime CreatedOnDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Capacity { get; set; }
        public int NumberOfParticipants { get; set; }
        public bool isDeleted { get; set; }
        public Guid OrganizerID { get; set; }
        public ApplicationUser Organizer { get; set; } = null!;
        public Category Category { get; set; } = null!;
    }
}
