using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Attendance.WebApi.Models
{
    public class VisitDTO
    {
        public int EventId { get; set; }
        public int PersonId { get; set; }
        public DateTime DateTime { get; set; }
        public int Id { get; set; }
    }
}
