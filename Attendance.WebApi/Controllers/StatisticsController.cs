namespace Attendance.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Attendance.DataAccess.DAL;
    using Attendance.DataAccess.Models;
    using Attendance.WebApi.Models;
    using System.Globalization;
    using Microsoft.AspNet.Identity.EntityFramework;

    [AllowAnonymous]
    public class StatisticsController : ApiController
    {
        /// <summary>
        /// Get a comprehensive set of event statistics
        /// </summary>
        /// <example>
        /// Invoke-RestMethod http://localhost/Attendance.WebApi/api/statistics/visits/comprehensive | Format-Table -AutoSize
        /// </example>
        /// <returns></returns>
        [Route("api/statistics/visits/comprehensive")]
        public IHttpActionResult GetVisitsComprehensive()
        {
            IEnumerable<dynamic> stats;
            using (AttendanceContext db = new AttendanceContext())
            {
                var query = db.Visits
                    .GroupBy(
                            v => new { Name = v.Event.Name, v.DateTime.Year, v.DateTime.Month }, // grouping terms
                            (key, group) => new { key.Name, key.Year, key.Month, Count = group.Count() } // projection
                    )
                    .ToLookup(
                        g => g.Name, // lookup key
                        g => new { g.Year, g.Month, g.Count } // projection
                    )
                    .Select(
                        l => new EventStatsDTO
                        {
                            Name = l.Key,
                            YearCounts = l
                                .GroupBy(x => x.Year, x => new { x.Month, x.Count })
                                .Select(x => new YearCount { Year = x.Key, MonthCounts = x.Select(y => new MonthCount() { Month = y.Month, MonthName = ConvertMonthNumberToMonthName(y.Month), Count = y.Count }) })
                        }
                    );

                stats = query.ToList();
            }

            return this.Ok(stats);
        }

        private string ConvertMonthNumberToMonthName(int monthNumber)
        {
            DateTimeFormatInfo dateInfo = new DateTimeFormatInfo();
            var fullName = dateInfo.GetMonthName(monthNumber);
            return fullName;
        }
    }
}
