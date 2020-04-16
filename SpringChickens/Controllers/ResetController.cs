using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.Database;
using Interfaces.Database.Entities;
using Interfaces.Factories;
using Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using SpringChickens.ViewModels;
using SpringChickens.ViewModels.Reset;

namespace SpringChickens.Controllers
{
    public class ResetController : Controller
    {
        private readonly IViewModelFactory _viewModelFactory;
        private readonly ICredentialHoldingService _credentialHoldingService;
        private readonly IPasswordResetService _passwordResetService;
        private readonly IUnitOfWork _context;

        public ResetController(
            IViewModelFactory viewModelFactory,
            ICredentialHoldingService credentialHoldingService,
            IPasswordResetService passwordResetService,
            IUnitOfWork context)
        {
            _viewModelFactory = viewModelFactory;
            _credentialHoldingService = credentialHoldingService;
            _passwordResetService = passwordResetService;
            _context = context;
        }

        public IActionResult Index(string errorMessage = "")
        {
            _credentialHoldingService.WipeService();

            var vm = _viewModelFactory.Resolve<ResetViewModel>();
            vm.ErrorMessage = errorMessage;

            return View(vm);
        }

        public IActionResult ChangePassword(string tokenString, string errorMessage = "")
        {
            var vm = _viewModelFactory.Resolve<ChangePasswordViewModel>();
            vm.ErrorMessage = errorMessage;
            vm.TokenString = tokenString;

            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult ChangePassword(ChangePasswordViewModel vm,string tokenString)
        {
            // Get and Validate token
            var errorMessage = "";

            IResetToken token;
            var isReal = _passwordResetService.GetToken(tokenString, out token);

            if (!isReal)
            {
                errorMessage += "Reset token invalid";
                return RedirectToAction("ChangePassword", new { tokenString = tokenString, errorMessage = errorMessage });
            }

            // Validate if dead

            var isDead = _passwordResetService.AuthenticateToken(token);

            if (isDead)
            {
                errorMessage += "Reset token invalid";
                return RedirectToAction("ChangePassword", new { tokenString = tokenString, errorMessage = errorMessage });
            }

            // Attempt to change password

            var success = _passwordResetService.UseToken(token, vm.NewPassword, out errorMessage);

            if (!success)
            {
                return RedirectToAction("ChangePassword", new { tokenString = tokenString, errorMessage = errorMessage });
            }


            var basevm = _viewModelFactory.Resolve<BaseViewModel>();
            return View("PasswordChanged", basevm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult SendEmail(ResetViewModel vm)
        {
            // Get user details from username
            IUser user;
            var userExists = _context.UserRepository.GetUserIfExists(vm.Username, out user);

            if (!userExists)
            {
                return RedirectToAction("Index", new { errorMessage = "User with that username does not exist." });
            }

            // Send email to user

            _passwordResetService.GenerateAndSendToken(user);

            var basevm = _viewModelFactory.Resolve<BaseViewModel>();
            return View("EmailSentConfirmation", basevm);
        }
    }
}