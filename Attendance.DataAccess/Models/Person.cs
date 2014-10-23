using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Attendance.DataAccess.Models
{
    public class Person
    {
        public int Id { get; set; }
        [Index("IX_FirstLastName", IsUnique = true, Order = 0)]
        [MaxLength(30)]
        public string FirstName { get; set; }
        [Index("IX_FirstLastName", IsUnique = true, Order = 1)]
        [MaxLength(30)]
        public string LastName { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
    }
}