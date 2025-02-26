﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STAREvents.Data.Models
{
    public class EventCategory
    {
        public Guid EventID { get; set; }
        [ForeignKey("EventID")]
        public Event Event { get; set; } = null!;


        public Guid CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public Category Category { get; set; } = null!;
    }
}
