using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.Services;
using Interfaces.Database.Entities;
using Interfaces.Database;
using System.Security.Policy;

namespace SpringChickens.Services
{
    public class UserService : IUserService
    {
        private readonly ICryptographyService _cryptographyService;
        private readonly IUnitOfWork _context;
        private readonly IPasswordFilterPredicate _passwordPredicate;
        private readonly IUsernameFilterPredicate _usernamePredicate;

        public UserService(
            ICryptographyService cryptographyService,
            IUnitOfWork context,
            IPasswordFilterPredicate passwordPredicate,
            IUsernameFilterPredicate usernamePredicate)
        {
            _cryptographyService = cryptographyService;
            _context = context;
            _passwordPredicate = passwordPredicate;
            _usernamePredicate = usernamePredicate;
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

        public bool ChangePassword(IUser user, string newPassword, out string errormsg)
        {
            if (!_passwordPredicate.Validate(newPassword, out errormsg))
            {
                return false;
            }

            var salt = _cryptographyService.GenerateSalt();

            var hashedPassword = _cryptographyService.Hash(newPassword + salt);

            return _context.UserRepository.ChangePassword(user, salt, hashedPassword);
        }

        public bool CreateNewUser(string username, string password, string email, out string errormsg)
        {

            if (_context.UserRepository.CheckIfUserExists(username))
            {
                errormsg = "Username has been taken.";
                return false;
            }

            if (!_usernamePredicate.Validate(username, out errormsg)) 
            {
                return false;
            }

            if (!_passwordPredicate.Validate(password, out errormsg))
            {
                return false;
            }

            var salt = _cryptographyService.GenerateSalt();

            var hashedPassword = _cryptographyService.Hash(password + salt);

            _context.UserRepository.CreateAndAddUser(username, hashedPassword, salt, email, false);

            _context.SaveChanges();

            errormsg = "";
            return true;

        }
    }
}
