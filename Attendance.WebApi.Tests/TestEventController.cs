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
    public class TestEventController
    {
        private EventController controller;
        public TestEventController()
        {
            this.controller = new EventController();
            controller.Request = new System.Net.Http.HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
        }

        [TestMethod]
        public void TestEventCRUD()
        {
            // create person and test id.
            var id1 = CreateEventAndReturnId(new EventDTO() { Name = "Test Event" });
            Assert.IsTrue(id1 > 0);

            // create person and test id.
            var id2 = CreateEventAndReturnId(new EventDTO() { Name = "Test Event" });
            Assert.IsTrue(id2 > 0);

            // get the first person we created
            var result = controller.GetEvent(id1); // returns IHttpActionResult
            var message = GetResponseMessageFromActionResult(result);
            var event1Id = GetEventDTOFromResponseMessage(message);
            Assert.IsTrue(id1 == event1Id.Id);

            // get both persons we created
            var enumerable = controller.GetAllEvents();
            var count = enumerable.Where<EventDTO>(p => p.Id == id1 || p.Id == id2).Count();
            Assert.AreEqual(count, 2);

            // delete both persons we created
            controller.DeleteEvent(id1);
            controller.DeleteEvent(id2);

            // make sure they're gone
            // get both persons we created
            enumerable = controller.GetAllEvents();
            count = enumerable.Where<EventDTO>(p => p.Id == id1 || p.Id == id2).Count();
            Assert.AreEqual(count, 0);
        }

        private int CreateEventAndReturnId(EventDTO dto)
        {
            var response = controller.PostEvent(dto); // returns HttpResponseMessage
            dto = GetEventDTOFromResponseMessage(response);
            return dto.Id;
        }

        private HttpResponseMessage GetResponseMessageFromActionResult(IHttpActionResult result)
        {
            return result.ExecuteAsync(new CancellationToken()).Result;
        }

        internal static EventDTO GetEventDTOFromResponseMessage(HttpResponseMessage msg)
        {
            var objContent = msg.Content as ObjectContent;
            var dto = objContent.Value as EventDTO;
            return dto;
        }
    }
}
