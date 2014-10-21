using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Attendance.DataAccess.Models;
using Attendance.DataAccess.DAL;
using Attendance.WebApi.Models;

namespace Attendance.WebApi.Controllers
{
    public class StatisticsController : ApiController
    {        
        /// Invoke-RestMethod http://localhost/Attendance.WebApi/api/stats/visits/all
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
            return Ok(stats);
        }
    }
}
