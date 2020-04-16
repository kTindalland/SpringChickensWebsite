using Interfaces.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Services
{
    public interface IUserService
    {
        bool AuthenticateLogin(string username, string password);

        bool CreateNewUser(string username, string password, string email, out string errormsg);

        bool ChangePassword(IUser user, string newPassword, out string errormsg);
    }
}
