using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Attendance.DataAccess.Models;

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
        
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Visit>().HasKey(e => new { e.PersonId, e.EventId });
        }
    }
}