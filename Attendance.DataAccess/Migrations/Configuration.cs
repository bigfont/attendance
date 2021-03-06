namespace Attendance.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Attendance.DataAccess.Models;
    using Attendance.DataAccess.DAL;

    internal sealed class Configuration : DbMigrationsConfiguration<AttendanceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;            
        }

        protected override void Seed(Attendance.DataAccess.DAL.AttendanceContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.Persons.AddOrUpdate(
              p => p.FirstName,
              new Person { FirstName = "Andrew Peters" },
              new Person { FirstName = "Brice Lambson" },
              new Person { FirstName = "Rowan Miller" }
            );

            context.Events.AddOrUpdate(
                e => e.Name,
                new Event() { Name = "Conjuring Club" },
                new Event() { Name = "Computer Club" } 
            );

            context.SaveChanges();

            var personId = context.Persons.FirstOrDefault().Id;
            var eventId = context.Events.FirstOrDefault().Id;

            context.Visits.AddOrUpdate(
                new Visit() { EventId = eventId, PersonId = personId, DateTime = DateTime.Now } );

            context.SaveChanges();
        }
    }
}
