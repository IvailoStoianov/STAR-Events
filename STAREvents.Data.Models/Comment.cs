using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static STAREvents.Common.EntityValidationConstants.CommentConstants;

namespace STAREvents.Data.Models
{
    public class Comment
    {
        public Comment()
        {
            CommentId = Guid.NewGuid();
        }
        [Key]
        public Guid CommentId { get; set; }

        [Required]
        public Guid EventId { get; set; }
        [ForeignKey("EventId")]
        public Event Event { get; set; } = null!;

        [Required]
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; } = null!;

        [Required]
        [MaxLength(MaxContentLength)]
        [MinLength(MinContentLength)]
        public string Content { get; set; } = null!;

        [Required]
        public DateTime PostedDate { get; set; } = DateTime.UtcNow;

        [Required]
        public bool isDeleted { get; set; } = false;
    }
}
