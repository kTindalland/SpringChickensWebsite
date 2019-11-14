using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SpringChickens.Models;
using Interfaces.Services;
using System.Net.Mail;
using System.Net;

namespace SpringChickens.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }

        public void SendEmail(string sendAddress)
        {
            var mailMessage = new MailMessage() {
                From = new MailAddress("springchickenswebsite@gmail.com")
            };

            mailMessage.To.Add(new MailAddress(sendAddress));

            mailMessage.Subject = "Hello Dad";

            mailMessage.IsBodyHtml = false;
            mailMessage.Body = "Is this working then";
            

            mailMessage.Priority = MailPriority.Normal;

            SmtpClient client = new SmtpClient();
            client.Credentials = new NetworkCredential("springchickenswebsite@gmail.com", "SuperSecurePassword123");
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Send(mailMessage);
        }
    }
}
