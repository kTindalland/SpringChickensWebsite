using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.Database;
using Interfaces.Factories;
using Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using SpringChickens.Factories;
using SpringChickens.ViewModels;
using SpringChickens.ViewModels.Admin;

namespace SpringChickens.Controllers
{
    public class AdminController : Controller
    {
        private IViewModelFactory _vmFactory;
        private readonly IUnitOfWork _context;
        private readonly ICredentialHoldingService _credentialHoldingService;

        public AdminController(
            IViewModelFactory vmFactory,
            IUnitOfWork context,
            ICredentialHoldingService credentialHoldingService)
        {
            _vmFactory = vmFactory;
            _context = context;
            _credentialHoldingService = credentialHoldingService;
        }

        public IActionResult Index()
        {
            // Validate is admin
            if (!_credentialHoldingService.IsAdmin) return RedirectToRoute(new { controller = "Home", action = "Index" });

            var vm = _vmFactory.Resolve<BaseViewModel>();

            return View(vm);
        }

        public IActionResult CarouselManagement()
        {
            var vm = _vmFactory.Resolve<CarouselManagementViewModel>();
            vm.AllCarouselItems = _context.CarouselItemRepository.GetAllItems();
            vm.ActiveCarouselItems = _context.CarouselItemRepository.GetAllActiveItems();

            return View(vm);
        }
    }
}