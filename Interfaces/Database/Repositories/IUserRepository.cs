using System;
using System.Collections.Generic;
using System.Text;
using Interfaces.Database.Entities;

namespace Interfaces.Database.Repositories
{
    public interface IUserRepository : IRepository<IUser>
    {
        // Specific User Repository features.
        bool GetUserIfExists(int id, out IUser result);

        bool GetUserIfExists(string username, out IUser result);
    }
}
