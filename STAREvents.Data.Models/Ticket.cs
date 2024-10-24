using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static STAREvents.Common.EntityValidationConstants.TicketConstants;
using Microsoft.VisualBasic;

namespace STAREvents.Data.Models
{
    public class Ticket
    {
        [Key]
        public int TicketID { get; set; }

        public int EventID { get; set; }

        [ForeignKey("EventID")]
        public Event Event { get; set; } = null!;

        public Guid UserID { get; set; }

        [ForeignKey("UserID")]
        public ApplicationUser User { get; set; } = null!;

        [Required]
        public DateTime PurchaseDate { get; set; }

        [Required]
        [Range(MinQuantity, MaxQuantity)]
        public int Quantity { get; set; }
    }
}
