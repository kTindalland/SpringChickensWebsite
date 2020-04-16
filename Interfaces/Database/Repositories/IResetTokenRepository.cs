using Interfaces.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Database.Repositories
{
    public interface IResetTokenRepository : IRepository<IResetToken>
    {
        void CleanTable();
        bool GenerateToken(IUser user, string tokenString, DateTime expiryTime);
        void CheckToken(IResetToken token, out bool isDead);
        IResetToken GetToken(string tokenString, out bool realToken);
        void KillToken(IResetToken token);
    }
}
