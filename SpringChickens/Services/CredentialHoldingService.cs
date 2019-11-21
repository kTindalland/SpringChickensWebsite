using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.Database.Entities;
using Interfaces.Services;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace SpringChickens.Services
{
    public class CredentialHoldingService : ICredentialHoldingService
    {
        public bool IsAdmin { get; private set; }

        public string Username { get; private set; }

        public string Email { get; private set; }

        public bool LoggedIn { get; private set; }

        public CredentialHoldingService()
        {
            LoggedIn = false;
        }

        public void PopulateService(IUser user)
        {
            IsAdmin = user.AdminRights;
            Username = user.UserName;
            Email = user.Email;

            LoggedIn = true;
        }

        public void WipeService()
        {
            IsAdmin = false;
            Username = null;
            Email = null;

            LoggedIn = false;
        }

        public void PopulateViewData(ViewDataDictionary viewData)
        {
            viewData["LoggedIn"] = LoggedIn;
            
            if (LoggedIn)
            {
                viewData["Username"] = Username;
                viewData["Email"] = Email;
                viewData["IsAdmin"] = IsAdmin;
            }
        }
    }
}
