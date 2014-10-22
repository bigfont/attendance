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
            // when exactly does this run? 
            // is it per application start? will it run again if we restart the application?
            Database.SetInitializer(new DropCreateDatabaseAlways<AttendanceContext>());
            Database.Initialize(false);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();                            
        }        
    }
}