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

    public class StatisticsController : ApiController
    {
        /// Invoke-RestMethod http://localhost/Attendance.WebApi/api/statistics/visits/all
        [Route("api/statistics/visits/all")]
        [HttpGet]
        public IHttpActionResult GetVisits()
        {
            IEnumerable<EventStatsDTO> stats = null;
            using (AttendanceContext db = new AttendanceContext())
            {
                var query = from e in db.Events
                            join v in db.Visits on e.Id equals v.EventId into visits
                            select new EventStatsDTO
                            {
                                Id = e.Id,
                                Name = e.Name,
                                VisitsTotal = visits.Count() // get all visits from all years
                            };
                stats = query.ToList();
            }

            return this.Ok(stats);
        }

        /// Invoke-RestMethod http://localhost/Attendance.WebApi/api/statistics/visits/month
        [Route("api/statistics/visits/month")]
        public IHttpActionResult GetVisitsByMonth()
        {
            IEnumerable<EventStatsDTO> stats;
            using (AttendanceContext db = new AttendanceContext())
            {
                var query = db.Visits
                    .GroupBy(v => new { v.EventId, v.DateTime.Month }, (k, g) => new { k.EventId, k.Month, Count = g.Count() })
                    .ToLookup(l => l.EventId, l => new { l.Month, l.Count })
                    .Select(l => new EventStatsDTO()
                    {
                        Id = l.Key,
                        VisitsByMonth = l.Select(x => x).ToDictionary(x=> x.Month, x => x.Count)
                    });
                stats = query.ToList();
            }

            return this.Ok(stats);
        }
    }
}
