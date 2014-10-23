using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Attendance.DataAccess.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;

namespace Attendance.DataAccess.DAL
{
    public class AttendanceContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public AttendanceContext()
            : base("AttendanceContext")
        {
            // the initializer runs once every application lifetime
            Database.SetInitializer(new CreateDatabaseIfNotExists<AttendanceContext>());
            Database.Initialize(false);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }        
    }
}