using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.Services;
using Interfaces.Database.Entities;
using Interfaces.Database;

namespace SpringChickens.Services
{
    public class UserService : IUserService
    {
        private readonly ICryptographyService _cryptographyService;
        private readonly IUnitOfWork _context;

        public UserService(ICryptographyService cryptographyService, IUnitOfWork context)
        {
            _cryptographyService = cryptographyService;
            _context = context;
        }

        public bool AuthenticateLogin(string username, string password)
        {
            IUser rec;

            if (!_context.UserRepository.GetUserIfExists(username, out rec))
            {
                return false;
            }

            var salt = rec.Salt;

            var unhashedSaltedPassword = password + salt;

            var hashedPass = _cryptographyService.Hash(unhashedSaltedPassword);

            return (hashedPass == rec.Hash);
        }
    }
}
