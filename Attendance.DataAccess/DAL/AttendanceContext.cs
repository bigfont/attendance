using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Attendance.DataAccess.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace Attendance.DataAccess.DAL
{
    public class AttendanceContext : IdentityDbContext<AttendanceUser>
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

        public static AttendanceContext Create()
        {
            return new AttendanceContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {                     
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }        
    }
}