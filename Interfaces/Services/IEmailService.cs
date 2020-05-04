using Interfaces.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Services
{
    public interface IEmailService
    {
        void SendEmail(string sendAddress);

        bool SendPasswordResetEmail(string sendAddress, string tokenString);

        void SendSubscriptionEmails(List<IUser> users, ITrip trip);
    }
}
