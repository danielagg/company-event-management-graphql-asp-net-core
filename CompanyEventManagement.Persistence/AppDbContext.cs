using CompanyEventManagement.Persistence.Entities;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CompanyEventManagement.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<User>().HasData(new[]
            {
                new User { Id = 1, Name = "Daniel Agg", CreatedOn = new DateTime(2019, 1, 1), EmailAddress = "dan@org.com", Position = UserPositionType.Developer },
                new User { Id = 2, Name = "Peter Smith", CreatedOn = new DateTime(2019, 2, 5), EmailAddress = "pete@org.com", Position = UserPositionType.Manager },
                new User { Id = 3, Name = "Jackie Taylor", CreatedOn = new DateTime(2019, 5, 23), EmailAddress = "jackie@org.com", Position = UserPositionType.SysAdmin }
            });
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<EventAttendee> Attendees { get; set; }
    }
}
