using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Attendance.WebApi.Services
{
    public interface IEmailService
    {
        void SendEmailAlert(string subject, string body);
    }

    public class EmailService : IEmailService
    {
        public void SendEmailAlert(string subject, string body)
        {
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            // see http://windows.microsoft.com/en-CA/windows/outlook/send-receive-from-app
            client.Host = "smtp-mail.outlook.com";
            client.Port = 587; // or 25, we use 587 when 25 is blocked
            client.EnableSsl = true; // equivalent to TLS         

            var password = ConfigurationManager.AppSettings["SmtpPassword"] as string;
            client.Credentials = new System.Net.NetworkCredential("bigfont@outlook.com", password);

            MailMessage mail = new MailMessage();
            mail.To.Add("bigfont@outlook.com");
            mail.From = new MailAddress("bigfont@outlook.com", "Attendance App");
            mail.Subject = subject;
            mail.Body = body;

            client.Send(mail);
        }
    }
}