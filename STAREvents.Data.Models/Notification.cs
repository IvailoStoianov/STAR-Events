using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static STAREvents.Common.EntityValidationConstants.NotificationConstants;

namespace STAREvents.Data.Models
{
    public class Notification
    {
        [Key]
        public Guid NotificationId { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(MaxContentLength)]
        public string Message { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [Required]
        public bool IsRead { get; set; } = false;
        [Required]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
        [Required]
        [ForeignKey(nameof(Event))]
        public Guid EventId { get; set; }
        public virtual Event Event { get; set; }
    }

}
