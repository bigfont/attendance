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
    public class PersonController : ApiController
    {
        public IEnumerable<PersonDTO> GetAllPersons()
        {
            AttendanceContext db = new AttendanceContext();
            IEnumerable<Person> personsDb = db.Persons;
            IEnumerable<PersonDTO> personsDTO = db.Persons.Select(p => new PersonDTO { 
                Id = p.Id,
                FirstName = p.FirstName, 
                LastName = p.LastName
            });
            return personsDTO;
        }

        public IHttpActionResult GetPerson(int id)
        {
            AttendanceContext db = new AttendanceContext();
            var person = db.Persons.FirstOrDefault(p => p.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        // $body = @{ FirstName = "Shaun"; LastName = "Luttin" } | ConvertTo-JSON
        // Invoke-RestMethod http://localhost/Attendance.WebApi/api/person -Method POST -ContentType "application/json" -Body $body -Debug
        public HttpResponseMessage PostPerson(PersonDTO personDTO)
        {
            // see also
            // http://www.asp.net/web-api/overview/creating-web-apis/using-web-api-1-with-entity-framework-5/using-web-api-with-entity-framework,-part-6

            var person = new Person() { 
                Id = personDTO.Id,
                FirstName = personDTO.FirstName, 
                LastName = personDTO.LastName
            };            

            using (AttendanceContext db = new AttendanceContext())
            {
                db.Persons.Add(person);
                db.SaveChanges();
            }

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, person);
            return response;
        }
    }
}
