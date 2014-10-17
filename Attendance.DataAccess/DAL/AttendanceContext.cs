using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Attendance.DataAccess.Models;
using System.ComponentModel.DataAnnotations.Schema;

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
            Database.SetInitializer(new DropCreateDatabaseAlways<AttendanceContext>());
        
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions                
                .Remove<PluralizingTableNameConvention>();            
        }        
    }
}