using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.WebApi.Tests
{
    [TestClass]
    public class TestEmailService
    {
        [TestMethod]
        public void TestEmailAlert()
        {
            Attendance.WebApi.Services.EmailService mailer = new Services.EmailService();
            mailer.SendEmailAlert("Test", "Test");
        }
    }
}
