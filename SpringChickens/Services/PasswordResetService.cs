using Database.Models;
using Interfaces.Database;
using Interfaces.Database.Entities;
using Interfaces.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringChickens.Services
{
    public class PasswordResetService : IPasswordResetService
    {
        private readonly IUnitOfWork _context;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;

        private readonly TimeSpan _expiryLength;

        public PasswordResetService(IUnitOfWork context, IEmailService emailService, IUserService userService)
        {
            _context = context;
            _emailService = emailService;
            _userService = userService;

            // 15 min expiry
            _expiryLength = new TimeSpan(0, 15, 0);
        }

        public bool AuthenticateToken(IResetToken token)
        {
            bool isDead;
            _context.ResetTokenRepository.CheckToken(token, out isDead);

            return isDead;
        }

        public void GenerateAndSendToken(IUser user)
        {
            var tokenString = Guid.NewGuid().ToString();

            var tokenMade = _context.ResetTokenRepository.GenerateToken(user, tokenString, DateTime.Now + _expiryLength);

            _emailService.SendPasswordResetEmail(user.Email, tokenString);
        }

        public bool GetToken(string tokenString, out IResetToken token)
        {
            bool realToken;
            token = _context.ResetTokenRepository.GetToken(tokenString, out realToken);

            // return if it's trustworthy
            return realToken;
        }

        public bool UseToken(IResetToken token, string newPassword, out string errormsg)
        {
            // get user from token
            IUser user;
            var userExists = _context.UserRepository.GetUserIfExists(token.UserId, out user);

            if (!userExists)
            {
                errormsg = "User doesn't exist.";
                return false;
            }

            // User service hashes password and gens salt
            bool success;
            success = _userService.ChangePassword(user, newPassword, out errormsg);

            // Kill token after use.
            _context.ResetTokenRepository.KillToken(token);

            return true;
        }
    }
}
