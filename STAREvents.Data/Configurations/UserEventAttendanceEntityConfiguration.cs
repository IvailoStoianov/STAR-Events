using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using STAREvents.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STAREvents.Data.Configurations
{
    public class UserEventAttendanceEntityConfiguration : IEntityTypeConfiguration<UserEventAttendance>
    {
        public void Configure(EntityTypeBuilder<UserEventAttendance> builder)
        {
            builder
                .HasKey(ec => new { ec.EventId, ec.UserId });

            builder
                .HasOne(ua => ua.Event)
                .WithMany(e => e.UserEventAttendances)
                .HasForeignKey(ua => ua.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(ua => ua.User)
                .WithMany(u => u.UserEventAttendances)
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
