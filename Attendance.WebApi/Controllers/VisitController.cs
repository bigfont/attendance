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
        public HttpResponseMessage PostVisit(int personId, int eventId)
        {            
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            return response;
        }

        public HttpResponseMessage DeleteVisit(int id)
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
