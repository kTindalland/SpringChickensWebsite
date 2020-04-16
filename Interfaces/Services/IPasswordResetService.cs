using Interfaces.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Services
{
    public interface IPasswordResetService
    {
        bool GetToken(string tokenString, out IResetToken token);

        void GenerateAndSendToken(IUser user);

        bool AuthenticateToken(IResetToken token);

        bool UseToken(IResetToken token, string newPassword, out string errormsg);
    }
}
