using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Attendance.WebApi.Models
{

    public class MonthCount
    {
        public int Month { get; set; }
        public int Count { get; set; }
    }

    public class YearCount
    {
        public int Year { get; set; }

        public IEnumerable<MonthCount> MonthCounts { get; set; }
    }

    public class EventStatsDTO
    {
        public string Name { get; set; }

        public IEnumerable<YearCount> YearCounts { get; set; }

    }
}
