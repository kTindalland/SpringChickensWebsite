﻿using Interfaces.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Services
{
    public interface IEmailService
    {
        bool SendPasswordResetEmail(string sendAddress, string tokenString);

        void SendSubscriptionEmails(List<IUser> users, ITrip trip);

        void SendContactMessage(string name, string email, string message);

        void SendUsernamesToEmail(List<string> usernames, string email);
    }
}
