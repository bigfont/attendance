﻿using System;
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
            // create two of both person and an event
            var personController = TestPersonController.Controller;
            var eventController = TestEventController.Controller;

            var personResponseMessage1 = personController.PostPerson(new PersonDTO() { FirstName = "FirstName", LastName = "LastName" });
            var tempPerson1 = TestPersonController.GetPersonDTOFromResponseMessage(personResponseMessage1);

            var personResponseMessage2 = personController.PostPerson(new PersonDTO() { FirstName = "FirstName2", LastName = "LastName2" });
            var tempPerson2 = TestPersonController.GetPersonDTOFromResponseMessage(personResponseMessage2);

            var eventResponseMessage1 = eventController.PostEvent(new EventDTO() { Name = "Name" });
            var tempEvent1 = TestEventController.GetEventDTOFromResponseMessage(eventResponseMessage1);

            var eventResponseMessage2 = eventController.PostEvent(new EventDTO() { Name = "Name2" });
            var tempEvent2 = TestEventController.GetEventDTOFromResponseMessage(eventResponseMessage2);

            // create visit
            var id1 = CreateVisitAndReturnId(new VisitDTO[] {
                new VisitDTO { PersonId = tempPerson1.Id, EventId = tempEvent1.Id, DateTime = DateTime.Now }
                ,new VisitDTO { PersonId = tempPerson2.Id, EventId = tempEvent1.Id, DateTime = DateTime.Now }
            });
            Assert.IsTrue(id1 >= 0);
        }

        private int CreateVisitAndReturnId(VisitDTO[] dto)
        {
            var result = controller.PostVisit(dto);
            var response = GetResponseMessageFromActionResult(result);

            dto = (response.Content as ObjectContent).Value as VisitDTO[];
            return dto.First().Id;            
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