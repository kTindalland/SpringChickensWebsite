using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Services
{
    public interface IUserService
    {
        bool AuthenticateLogin(string username, string password);

        bool CreateNewUser(string username, string password, out string errormsg);
    }
}
