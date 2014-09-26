using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Attendance.DataAccess.Models;
using Attendance.DataAccess.DAL;

namespace Attendance.WebApi.Controllers
{
    public class PersonController : ApiController
    {
        private Person[] persons = new Person[] { 
        
            new Person() { Id = 01, FirstName = "Anjali", LastName = "Moreau" }, 
            new Person() { Id = 02, FirstName = "Noah", LastName = "Jacobson" }        
        };

        public IEnumerable<Person> GetAllPersons()
        {
            AttendanceContext db = new AttendanceContext();
            IEnumerable<Person> personsDb = db.Persons;
            return personsDb;
        }

        public IHttpActionResult GetPerson(int id)
        {
            var person = persons.FirstOrDefault(p => p.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }
    }
}
