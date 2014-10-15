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
        /// Invoke-RestMethod http://localhost/Attendance.WebApi/api/person -Method GET
        public IEnumerable<PersonDTO> GetAllPersons()
        {
            IEnumerable<PersonDTO> personsDTO;
            using (AttendanceContext db = new AttendanceContext())
            {
                var personsDb = db.Persons;
                personsDTO = db.Persons.Select(p => new PersonDTO
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName
                }).ToList();
            }
            return personsDTO;
        }

        /// Invoke-RestMethod http://localhost/Attendance.WebApi/api/person/1 -Method GET
        public IHttpActionResult GetPerson(int id)
        {
            AttendanceContext db = new AttendanceContext();
            var person = db.Persons.FirstOrDefault(p => p.Id == id);
            var personDTO = new PersonDTO() { 
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName
            };

            if (person == null)
            {
                return NotFound();
            }
            return Ok(personDTO);
        }

        /// $body = @{ FirstName = "Shaun"; LastName = "Luttin" } | ConvertTo-JSON
        /// Invoke-RestMethod http://localhost/Attendance.WebApi/api/person -Method POST -ContentType "application/json" -Body $body -Debug
        public HttpResponseMessage PostPerson(PersonDTO personDTO)
        {
            // see also
            // http://www.asp.net/web-api/overview/creating-web-apis/using-web-api-1-with-entity-framework-5/using-web-api-with-entity-framework,-part-6

            var person = new Person()
            {
                Id = personDTO.Id,
                FirstName = personDTO.FirstName,
                LastName = personDTO.LastName
            };

            using (AttendanceContext db = new AttendanceContext())
            {
                db.Persons.Add(person);
                db.SaveChanges();
            }

            personDTO.Id = person.Id;

            HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.Created, personDTO);
            return response;
        }

        /// Invoke-RestMethod http://localhost/Attendance.WebApi/api/person/5 -Method DELETE
        public HttpResponseMessage DeletePerson(int id)
        {
            using (AttendanceContext db = new AttendanceContext())
            {
                var person = new Person() { Id = id };
                db.Persons.Attach(person);
                db.Persons.Remove(person);
                db.SaveChanges();
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
