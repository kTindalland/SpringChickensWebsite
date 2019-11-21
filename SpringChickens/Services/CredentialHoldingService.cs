using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.Database.Entities;
using Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SpringChickens.Services
{
    public class CredentialHoldingService : ICredentialHoldingService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public bool IsAdmin
        {
            get => "true" == _httpContextAccessor.HttpContext.Session.GetString("IsAdmin");
            private set
            {
                _httpContextAccessor.HttpContext.Session.SetString("IsAdmin", value ? "true" : "false");
            }
        }

        public string Username 
        {
            get => _httpContextAccessor.HttpContext.Session.GetString("Username");

            private set
            {
                _httpContextAccessor.HttpContext.Session.SetString("Username", value);
            }
        }

        public string Email 
        {
            get => _httpContextAccessor.HttpContext.Session.GetString("Email");

            private set
            {
                _httpContextAccessor.HttpContext.Session.SetString("Email", value);
            }
        }

        public bool LoggedIn 
        { 
            get => "true" == _httpContextAccessor.HttpContext.Session.GetString("LoggedIn");
            private set
            {
                _httpContextAccessor.HttpContext.Session.SetString("LoggedIn", value ? "true" : "false");
            }
        }



        public CredentialHoldingService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            LoggedIn = false;
        }

        public void PopulateService(IUser user)
        {
            IsAdmin = user.AdminRights;
            Username = user.UserName;
            Email = user.Email ?? "NO EMAIL GIVEN";

            LoggedIn = true;
        }

        public void WipeService()
        {
            IsAdmin = false;
            Username = null;
            Email = null;

            LoggedIn = false;
        }
    }
}
