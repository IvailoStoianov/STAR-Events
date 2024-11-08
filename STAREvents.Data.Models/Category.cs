﻿using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static STAREvents.Common.EntityValidationConstants.CategoryConstants;

namespace STAREvents.Data.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [Required]
        [MaxLength(MaxNameLength)]
        [MinLength(MinNameLength)]
        public string Name { get; set; } = null!;

        public ICollection<EventCategory> EventCategories { get; set; }
            = new HashSet<EventCategory>();
    }
}
