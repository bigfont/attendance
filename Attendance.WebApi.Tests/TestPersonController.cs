using Attendance.WebApi.Controllers;
using Attendance.WebApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Attendance.WebApi.Tests
{
    [TestClass]
    public class TestPersonController
    {
        [TestMethod]
        public void TestPostPerson()
        {
            System.Diagnostics.Debugger.Break();

            PersonDTO dto = new PersonDTO()
            {
                FirstName = "Test First Name",
                LastName = "Test Last Name"
            };

            var controller = new PersonController();


            //var response = controller.PostPerson(dto);
        }
    }
}
