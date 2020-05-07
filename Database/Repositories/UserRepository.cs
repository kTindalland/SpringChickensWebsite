using System;
using System.Collections.Generic;
using System.Text;
using Interfaces.Database.Entities;
using Interfaces.Database.Repositories;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Interfaces.Services;

namespace Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext _context;

        private DatabaseContext Context { get => (DatabaseContext)_context; }

        public UserRepository(DbContext context)
        {
            _context = context;
        }

        public bool Add(IUser entity)
        {
            Context.Add((User)entity);

            return true;
        }

        public bool GetUserIfExists(int id, out IUser result)
        {
            if (!Context.Users.Any(r => r.Id == id))
            {
                result = null;
                return false;
            }

            result = Context.Users.First(r => r.Id == id);

            return true;
        }

        public bool GetUserIfExists(string username, out IUser result)
        {
            if (!Context.Users.Any(r => r.UserName == username))
            {
                result = null;
                return false;
            }

            result = Context.Users.First(r => r.UserName == username);

            return true;
        }

        public bool CheckIfUserExists(string username)
        {
            return Context.Users.Any(r => r.UserName == username);
        }

        public bool CheckIfUserExists(int id)
        {
            return Context.Users.Any(r => r.Id == id);
        }

        public void CreateAndAddUser(string username, string hash, string salt, string email, bool adminRights)
        {
            var newUser = new User()
            {
                UserName = username,
                Hash = hash,
                Salt = salt,
                AdminRights = adminRights,
                Email = email
            };

            Context.Users.Add(newUser);
        }

        public List<IUser> GetAllAdmins()
        {
            var adminUsers = Context.Users.Where(r => r.AdminRights).ToList<IUser>();

            return adminUsers;
        }

        public List<IUser> GetAllNonAdmins()
        {
            var nonAdminUsers = Context.Users.Where(r => !r.AdminRights).ToList<IUser>();

            return nonAdminUsers;
        }

        public bool ChangePassword(IUser user, string newSalt, string newPassword)
        {
            if (Context.Users.Any(r => r.Id == user.Id))
            {
                var record = Context.Users.First(r => r.Id == user.Id);

                record.Salt = newSalt;
                record.Hash = newPassword;

                Context.SaveChanges();

                return true;
            }
            return false;
        }

        public List<IUser> GetAllUsers()
        {
            var allRecords = Context.Users.ToList<IUser>();

            return allRecords;
        }

        public void ChangeAdminStatus(int id, bool adminStatus)
        {
            // Get if exists
            IUser user;
            var valid = GetUserIfExists(id, out user);

            // Exit out if not a valid user
            if (!valid) return;

            // valid, so change admin status

            user.AdminRights = adminStatus;

            Context.Users.Update((User)user);

            Context.SaveChanges();
        }

        public List<string> GetAllUsernamesFromEmail(string email)
        {
            var users = Context.Users.Where(r => r.Email == email).Select(r => r.UserName).ToList();

            return users;
        }
    }
}
