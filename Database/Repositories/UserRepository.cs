using System;
using System.Collections.Generic;
using System.Text;
using Interfaces.Database.Entities;
using Interfaces.Database.Repositories;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
    }
}
