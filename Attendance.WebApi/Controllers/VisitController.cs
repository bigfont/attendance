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
        /// $body = @{ PersonId = "1"; EventId = "1"; DateTime = "" } | ConvertTo-JSON
        /// Invoke-RestMethod http://localhost/Attendance.WebApi/api/visit -Method POST -ContentType "application/json" -Body $body -Debug
        /// 
        /// Notes on Sending Dates with REST.
        /// var date = new Date();
        /// var jsTimestamp = date.getTime();
        /// var iso8601 = date.toJSON();
        /// console.log(jsTimestamp); // 1412978008444
        /// console.log(iso8601); // 2014-10-10T21:54:47.453Z
        public HttpResponseMessage PostVisit(VisitDTO visitDTO)
        {
            var visit = new Visit()
            {
                PersonId = visitDTO.PersonId,
                EventId = visitDTO.EventId,
                DateTime = visitDTO.DateTime
            };

            using (AttendanceContext db = new AttendanceContext())
            {
                db.Visits.Add(visit);
                db.SaveChanges();
            }

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, visit);
            return response;
        }

        public HttpResponseMessage DeleteVisit(int id)
        {
            throw new NotImplementedException();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
