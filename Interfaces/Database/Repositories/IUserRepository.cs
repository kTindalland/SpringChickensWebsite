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

        bool CheckIfUserExists(string username);

        bool CheckIfUserExists(int id);

        void CreateAndAddUser(string username, string hash, string salt, string email, bool adminRights);

        List<IUser> GetAllAdmins();

        List<IUser> GetAllNonAdmins();

        bool ChangePassword(IUser user, string newSalt, string newPassword);

        List<IUser> GetAllUsers();

        void ChangeAdminStatus(int id, bool adminStatus);

        List<string> GetAllUsernamesFromEmail(string email);
    }
}
