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
    public class VisitController : ApiController
    {
        /// $body = @( @{ PersonId = "1"; EventId = "1"; DateTime = "2014-01-01" }, @{ PersonId = "2"; EventId = "2"; DateTime = "2014-01-01" } ) | ConvertTo-JSON
        /// Invoke-RestMethod http://localhost/Attendance.WebApi/api/visits -Method POST -ContentType "application/json" -Body $body -Debug
        /// 
        /// Notes on Sending Dates with REST.
        /// var date = new Date();
        /// var jsTimestamp = date.getTime();
        /// var iso8601 = date.toJSON();
        /// console.log(jsTimestamp); // 1412978008444
        /// console.log(iso8601); // 2014-10-10T21:54:47.453Z
        public IHttpActionResult PostVisit(VisitDTO[] visitDTOs)
        {
            var visits = visitDTOs.Select(v => new Visit()
            {                
                PersonId = v.PersonId,
                EventId = v.EventId,
                DateTime = v.DateTime
            });

            using (AttendanceContext db = new AttendanceContext())
            {
                db.Visits.AddRange(visits);
                db.SaveChanges();
            }

            return this.Created("string location", visitDTOs);
        }

        public IHttpActionResult DeleteVisit(int id)
        {
            throw new NotImplementedException();
        }

        public IHttpActionResult GetAllVisits()
        {
            throw new NotImplementedException();
        }

        public IHttpActionResult DeleteVisit(int id1)
        {
            throw new NotImplementedException();
        }
    }
}
