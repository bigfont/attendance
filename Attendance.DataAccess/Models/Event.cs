using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Attendance.DataAccess.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Visit> Visits { get; set; }
    }
}