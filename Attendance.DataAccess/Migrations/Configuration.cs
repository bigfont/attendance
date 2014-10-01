namespace Attendance.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Attendance.DataAccess.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Attendance.DataAccess.DAL.AttendanceContext>
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
            
        }
    }
}