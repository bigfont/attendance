using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Attendance.WebApi.Models
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}