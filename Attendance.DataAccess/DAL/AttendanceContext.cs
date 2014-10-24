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
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class AttendanceUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AttendanceUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

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
        }        
    }
}