using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SpringChickens.Models;
using Interfaces.Services;
using System.Net.Mail;
using System.Net;
using Interfaces.Database.Entities;

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

        public bool SendPasswordResetEmail(string emailAddress, string tokenString)
        {
            var mailMessage = new MailMessage()
            {
                From = new MailAddress("springchickenswebsite@gmail.com")
            };

            mailMessage.To.Add(new MailAddress(emailAddress));

            mailMessage.Subject = "Spring Chickens password reset";

            mailMessage.IsBodyHtml = true;
            mailMessage.Body = $"Please follow this link to reset your password.\n<a href=\"https://localhost:44335/Reset/ChangePassword?tokenString={tokenString} \">reset password</a>";


            mailMessage.Priority = MailPriority.Normal;

            SmtpClient client = new SmtpClient();
            client.Credentials = new NetworkCredential("springchickenswebsite@gmail.com", "SuperSecurePassword123");
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Send(mailMessage);

            return true;
        }

        public void SendSubscriptionEmails(List<IUser> users, ITrip trip)
        {
            SmtpClient client = new SmtpClient();
            client.Credentials = new NetworkCredential("springchickenswebsite@gmail.com", "SuperSecurePassword123");
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;

            foreach (var user in users)
            {
                var mail = GenerateSubscriptionMessage(user, trip);
                client.Send(mail);
            }

        }

        private MailMessage GenerateSubscriptionMessage(IUser user, ITrip trip)
        {
            var mailMessage = new MailMessage()
            {
                From = new MailAddress("springchickenswebsite@gmail.com")
            };

            mailMessage.To.Add(new MailAddress(user.Email));

            mailMessage.Subject = $"Spring Chickens made a new post in {trip.TripName}";

            mailMessage.IsBodyHtml = true;
            mailMessage.Body = $"There was a new post in a trip you're subscribed to {user.UserName}!\n" +
                $"Click <a href=\"https://localhost:44335/Trip/ViewTrip/{trip.Id}\">here</a> to go to the updated trip.\n\n\n" +
                $"Want to unsubscribe from {trip.TripName}? <a href=\"https://localhost:44335/Trip\">Click here to go to the page to unsubscribe</a>\n" +
                $"<p style=\"color:red\">You need to be signed in to unsubscribe.</p>";


            mailMessage.Priority = MailPriority.Normal;

            return mailMessage;
        }
    }
}
