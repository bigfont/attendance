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
    public class TestVisitController
    {
        private VisitController controller;

        public TestVisitController()
        {
            this.controller = new VisitController();
            controller.Request = new System.Net.Http.HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
        }

        [TestMethod]
        public void TestVisitCRUD()
        {
            // create a person and an event
            var personController = TestPersonController.Controller;
            var eventController = TestEventController.Controller;

            var personResponseMessage = personController.PostPerson(new PersonDTO() { FirstName = "FirstName", LastName = "LastName" });
            var tempPerson = TestPersonController.GetPersonDTOFromResponseMessage(personResponseMessage);

            var eventResponseMessage = eventController.PostEvent(new EventDTO() { Name = "Name" });
            var tempEvent = TestEventController.GetEventDTOFromResponseMessage(eventResponseMessage);

            // create visit
            var id1 = CreateVisitAndReturnId(new VisitDTO() { PersonId = tempPerson.Id, EventId = tempEvent.Id, DateTime = DateTime.Now });
            Assert.IsTrue(id1 >= 0);

            //// create person and test id.
            //var id2 = CreateVisitAndReturnId(new PersonDTO() { FirstName = "Shaun", LastName = "Luttin" });
            //Assert.IsTrue(id2 > 0);

            //// get the first person we created
            //var result = controller.GetVisit(id1); // returns IHttpActionResult
            //var message = GetResponseMessageFromActionResult(result);
            //var person1 = GetVisitDTOFromResponseMessage(message);
            //Assert.IsTrue(id1 == person1.Id);

            //// get both persons we created
            //var enumerable = controller.GetAllVisits();
            //var count = enumerable.Where<VisitDTO>(p => p.Id == id1 || p.Id == id2).Count();
            //Assert.AreEqual(count, 2);

            //// delete both persons we created
            //controller.DeletePerson(id1);
            //controller.DeletePerson(id2);

            //// make sure they're gone
            //// get both persons we created
            //enumerable = controller.GetAllVisits();
            //count = enumerable.Where<VisitDTO>(p => p.Id == id1 || p.Id == id2).Count();
            //Assert.AreEqual(count, 0);
        }

        private int CreateVisitAndReturnId(VisitDTO dto)
        {
            var response = controller.PostVisit(dto); // returns HttpResponseMessage
            dto = GetVisitDTOFromResponseMessage(response);
            return dto.Id;
        }

        private HttpResponseMessage GetResponseMessageFromActionResult(IHttpActionResult result)
        {
            return result.ExecuteAsync(new CancellationToken()).Result;
        }

        private VisitDTO GetVisitDTOFromResponseMessage(HttpResponseMessage msg)
        {
            var objContent = msg.Content as ObjectContent;
            var dto = objContent.Value as VisitDTO;
            return dto;
        }
    }

}