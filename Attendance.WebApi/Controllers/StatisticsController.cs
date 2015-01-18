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
            IEnumerable<EventStatsDTO> stats;
            using (AttendanceContext db = new AttendanceContext())
            {
                var query = db.Visits
                    .GroupBy(
                        v => new { v.EventId, EventName = v.Event.Name, v.DateTime.Month }, // grouping terms
                        (key, group) => new { key.EventId, key.EventName, key.Month, Count = group.Count() }) // projection
                    .ToLookup(
                        l => new { l.EventId, l.EventName }, 
                        l => new { l.Month, l.Count })
                    .Select(
                        l => new EventStatsDTO()
                            {
                                Id = l.Key.EventId,
                                Name = l.Key.EventName,
                                VisitsSinceInception = l.Sum(x => x.Count),
                                VisitsByMonth = l.Select(x => x).ToDictionary(x => ConvertMonthNumberIntoAbbreviatedMonthName(x.Month), x => x.Count)
                            });
                stats = query.ToList();
            }

            return this.Ok(stats);
        }

        private string ConvertMonthNumberIntoAbbreviatedMonthName(int monthNumber)
        {
            DateTimeFormatInfo dateInfo = new DateTimeFormatInfo();
            var fullName = dateInfo.GetMonthName(monthNumber);
            var abbrName = fullName.Substring(0, 3);
            return abbrName;            
        }
    }
}
