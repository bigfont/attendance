using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Attendance.WebApi.Controllers;
using Attendance.WebApi.Models;

namespace Attendance.WebApi.Tests
{
    [TestClass]
    public class TestPersonController
    {
        internal static PersonController Controller;

        static TestPersonController()
        {
            TestPersonController.Controller = new PersonController();
            TestPersonController.Controller.Request = new System.Net.Http.HttpRequestMessage();
            TestPersonController.Controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
        }

        [TestMethod]
        public void TestPersonCRUD()
        {
            // create person and test id.
            var id1 = CreatePersonAndReturnId(new PersonDTO() { FirstName = "Shaun", LastName = "Luttin" });
            Assert.IsTrue(id1 >= 0);

            // create person and test id.
            var id2 = CreatePersonAndReturnId(new PersonDTO() { FirstName = "Shaun", LastName = "Luttin" });
            Assert.IsTrue(id2 >= 0);

            // get the first person we created
            var result = TestPersonController.Controller.GetPerson(id1); // returns IHttpActionResult
            var message = GetResponseMessageFromActionResult(result);
            var person1 = GetPersonDTOFromResponseMessage(message);
            Assert.IsTrue(id1 == person1.Id);

            // get both persons we created
            var enumerable = TestPersonController.Controller.GetAllPersons();
            var count = enumerable.Where<PersonDTO>(p => p.Id == id1 || p.Id == id2).Count();
            Assert.AreEqual(count, 2);

            // delete both persons we created
            TestPersonController.Controller.DeletePerson(id1);
            TestPersonController.Controller.DeletePerson(id2);

            // make sure they're gone
            // get both persons we created
            enumerable = TestPersonController.Controller.GetAllPersons();
            count = enumerable.Where<PersonDTO>(p => p.Id == id1 || p.Id == id2).Count();
            Assert.AreEqual(count, 0);
        }

        private int CreatePersonAndReturnId(PersonDTO dto)
        {
            var response = TestPersonController.Controller.PostPerson(dto); // returns HttpResponseMessage
            dto = GetPersonDTOFromResponseMessage(response);
            return dto.Id;
        }

        private HttpResponseMessage GetResponseMessageFromActionResult(IHttpActionResult result)
        {
            return result.ExecuteAsync(new CancellationToken()).Result;
        }

        internal static PersonDTO GetPersonDTOFromResponseMessage(HttpResponseMessage msg)
        {
            var objContent = msg.Content as ObjectContent;
            var dto = objContent.Value as PersonDTO;
            return dto;
        }
    }

}