using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Attendance.DataAccess.Models
{
    public class Visit
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int EventId { get; set; }
        public DateTime DateTime { get; set; }
        public virtual Person Person { get; set; }
        public virtual Event Event { get; set; }
    }
}