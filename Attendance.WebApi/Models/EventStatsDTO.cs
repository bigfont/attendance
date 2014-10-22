using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Attendance.WebApi.Models
{

    public class EventStatsDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<KeyValuePair<string, int>> VisitsByMonth { get; set; }

        public int VisitsSinceInception { get; set; }
    }
}
