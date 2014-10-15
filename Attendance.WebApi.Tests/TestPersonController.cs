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
        private PersonController controller;

        public TestPersonController()
        {
            this.controller = new PersonController();
            controller.Request = new System.Net.Http.HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());        
        }

        [TestMethod]
        public void TestPostCRUD()
        {
            // create person and test id.
            var id1 = CreatePersonAndReturnId(new PersonDTO() { FirstName = "Shaun", LastName = "Luttin" });
            Assert.IsTrue(id1 > 0);

            // create person and test id.
            var id2 = CreatePersonAndReturnId(new PersonDTO() { FirstName = "Shaun", LastName = "Luttin" });
            Assert.IsTrue(id2 > 0);

            // get the first person we created
            var result = controller.GetPerson(id1); // returns IHttpActionResult
            var message = GetResponseMessageFromActionResult(result);
            var person1 = GetPersonDTOFromResponseMessage(message);
            Assert.IsTrue(id1 == person1.Id);

            // get both persons we created
            var enumerable = controller.GetAllPersons();
            var count = enumerable.Where<PersonDTO>(p => p.Id == id1 || p.Id == id2).Count();
            Assert.AreEqual(count, 2);

            // delete both persons we created
            controller.DeletePerson(id1);
            controller.DeletePerson(id2);

            // make sure they're gone
            // get both persons we created
            enumerable = controller.GetAllPersons();
            count = enumerable.Where<PersonDTO>(p => p.Id == id1 || p.Id == id2).Count();
            Assert.AreEqual(count, 0);
        }

        private int CreatePersonAndReturnId(PersonDTO personDTO)
        {            
            var response = controller.PostPerson(personDTO); // returns HttpResponseMessage
            personDTO = GetPersonDTOFromResponseMessage(response);
            return personDTO.Id;
        }

        private HttpResponseMessage GetResponseMessageFromActionResult(IHttpActionResult result)
        {
            return result.ExecuteAsync(new CancellationToken()).Result;
        }

        private PersonDTO GetPersonDTOFromResponseMessage(HttpResponseMessage msg)
        {
            var objContent = msg.Content as ObjectContent;
            var person = objContent.Value as PersonDTO;
            return person;
        }
    }
}
