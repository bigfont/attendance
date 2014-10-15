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
    public class EventController : ApiController
    {
        /// Invoke-RestMethod http://localhost/Attendance.WebApi/api/event -Method GET
        public IEnumerable<EventDTO> GetAllEvents()
        {
            IEnumerable<EventDTO> eventsDTO = null;
            using (AttendanceContext db = new AttendanceContext())
            {
                eventsDTO = db.Events.Select(e => new EventDTO
                {
                    Id = e.Id,
                    Name = e.Name
                }).ToList();
            }
            return eventsDTO;
        }

        /// Invoke-RestMethod http://localhost/Attendance.WebApi/api/event/1 -Method GET
        public IHttpActionResult GetEvent(int id)
        {
            Event ev = null;
            using (var db = new AttendanceContext())
            {
                ev = db.Events.FirstOrDefault(e => e.Id == id);
            }

            var eventDTO = new EventDTO()
            {
                Id = ev.Id,
                Name = ev.Name
            };            

            if (ev == null)
            {
                return NotFound();
            }
            return Ok(eventDTO);
        }

        /// $body = @{ Name = "Conjuring Club" } | ConvertTo-JSON
        /// Invoke-RestMethod http://localhost/Attendance.WebApi/api/event -Method POST -ContentType "application/json" -Body $body -Debug
        public HttpResponseMessage PostEvent(EventDTO eventDTO)
        {
            // see also
            // http://www.asp.net/web-api/overview/creating-web-apis/using-web-api-1-with-entity-framework-5/using-web-api-with-entity-framework,-part-6

            var ev = new Event()
            {
                Id = eventDTO.Id,
                Name = eventDTO.Name
            };

            using (AttendanceContext db = new AttendanceContext())
            {
                db.Events.Add(ev);
                db.SaveChanges();
            }

            eventDTO.Id = ev.Id;

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, eventDTO);
            return response;
        }

        /// Invoke-RestMethod http://localhost/Attendance.WebApi/api/event/5 -Method DELETE
        public HttpResponseMessage DeleteEvent(int id)
        {
            using (AttendanceContext db = new AttendanceContext())
            {
                var ev = new Event() { Id = id };
                db.Events.Attach(ev);
                db.Events.Remove(ev);
                db.SaveChanges();
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
