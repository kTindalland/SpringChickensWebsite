using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.Database;
using Interfaces.Factories;
using Microsoft.AspNetCore.Mvc;
using SpringChickens.Factories;
using SpringChickens.ViewModels.Admin;

namespace SpringChickens.Controllers
{
    public class AdminController : Controller
    {
        private IViewModelFactory _vmFactory;
        private readonly IUnitOfWork _context;

        public AdminController(IViewModelFactory vmFactory, IUnitOfWork context)
        {
            _vmFactory = vmFactory;
            _context = context;
        }

        public IActionResult ListUsers()
        {
            var vm = _vmFactory.Resolve<ListUsersViewModel>();
            vm.AdminUsers = _context.UserRepository.GetAllAdmins();
            vm.NormalUsers = _context.UserRepository.GetAllNonAdmins();

            return View(vm);
        }
    }
}