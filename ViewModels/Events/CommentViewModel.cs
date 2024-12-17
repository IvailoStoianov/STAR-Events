using STAREvents.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STAREvents.Web.ViewModels.Events
{
    public class CommentViewModel
    {
        public Guid CommentId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime PostedDate { get; set; }
        public ApplicationUser User { get; set; } = null!;
        public bool isDeleted { get; set; }
    }
}
