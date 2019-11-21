using System;
using System.Collections.Generic;
using System.Text;
using Interfaces.Database.Entities;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Interfaces.Services
{
    public interface ICredentialHoldingService
    {
        void PopulateService(IUser user);

        void WipeService();

        bool LoggedIn { get; }

        bool IsAdmin { get; }

        string Username { get; }

        string Email { get; }
    }
}
