using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static STAREvents.Common.EntityValidationConstants.TagConstants;

namespace STAREvents.Data.Models
{
    public class Tag
    {
        [Key]
        public Guid TagId { get; set; }

        [Required]
        [MaxLength(MaxTagLength)]
        [MinLength(MinTagLength)]
        public string Name { get; set; } = null!;

    }
}
