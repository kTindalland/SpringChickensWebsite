using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using Interfaces.Database;
using Interfaces.Database.Entities;
using Interfaces.Factories;
using Interfaces.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SpringChickens.Factories;
using SpringChickens.ViewModels;
using SpringChickens.ViewModels.Admin;
using SpringChickens.ViewModels.Partials;

namespace SpringChickens.Controllers
{
    public class AdminController : Controller
    {
        private IViewModelFactory _vmFactory;
        private readonly IUnitOfWork _context;
        private readonly ICredentialHoldingService _credentialHoldingService;
        private readonly IWebHostEnvironment _env;
        private readonly ICalendarHelpingService _calendarHelpingService;
        private readonly ISearchUsersService _searchUsersService;

        public AdminController(
            IViewModelFactory vmFactory,
            IUnitOfWork context,
            ICredentialHoldingService credentialHoldingService,
            IWebHostEnvironment env,
            ICalendarHelpingService calendarHelpingService,
            ISearchUsersService searchUsersService)
        {
            _vmFactory = vmFactory;
            _context = context;
            _credentialHoldingService = credentialHoldingService;
            _env = env;
            _calendarHelpingService = calendarHelpingService;
            _searchUsersService = searchUsersService;
        }

        private CarouselManagementViewModel ResolveViewModel()
        {
            var vm = _vmFactory.Resolve<CarouselManagementViewModel>();
            vm.AllCarouselItems = _context.CarouselItemRepository.GetAllItems();
            vm.ActiveCarouselItems = _context.CarouselItemRepository.GetAllActiveItems();

            return vm;
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
            // Validate is admin
            if (!_credentialHoldingService.IsAdmin) return RedirectToRoute(new { controller = "Home", action = "Index" });

            return View(ResolveViewModel());
        }

        #region Carousel Actions

        public IActionResult CarouselMoveUp(int id)
        {
            // Validate is admin
            if (!_credentialHoldingService.IsAdmin) return RedirectToRoute(new { controller = "Home", action = "Index" });

            _context.CarouselItemRepository.MoveItemUp(id);

            return RedirectToAction("CarouselManagement");
        }

        public IActionResult CarouselMoveDown(int id)
        {
            // Validate is admin
            if (!_credentialHoldingService.IsAdmin) return RedirectToRoute(new { controller = "Home", action = "Index" });

            _context.CarouselItemRepository.MoveItemDown(id);

            return RedirectToAction("CarouselManagement");
        }

        public IActionResult CarouselDelete(int id)
        {
            // Validate is admin
            if (!_credentialHoldingService.IsAdmin) return RedirectToRoute(new { controller = "Home", action = "Index" });

            // Delete photo if it exists
            var filename = _context.CarouselItemRepository.GetPhotoFileName(id);
            if (filename != "RecordNonExistant")
            {
                System.IO.File.Delete($"{ _env.WebRootPath}/carouselImages/{filename}");
            }

            // Delete record by id
            _context.CarouselItemRepository.DeleteItem(id);

            

            return RedirectToAction("CarouselManagement");
        }

        public IActionResult CarouselFlipActivation(int id)
        {
            // Validate is admin
            if (!_credentialHoldingService.IsAdmin) return RedirectToRoute(new { controller = "Home", action = "Index" });

            _context.CarouselItemRepository.FlipActivation(id);

            return RedirectToAction("CarouselManagement");
        }

        #endregion

        public IActionResult Calendar()
        {
            // Validate is admin
            if (!_credentialHoldingService.IsAdmin) return RedirectToRoute(new { controller = "Home", action = "Index" });

            var year = _calendarHelpingService.Year;
            var month = _calendarHelpingService.Month;
            var showOutdatedEvents = _calendarHelpingService.ShowOutdatedEvents;

            var vm = _vmFactory.Resolve<CalendarManagementViewModel>();

            DateTime now;
            List<ICalendarEvent> events;

            if (year < 1 || month < 1)
            {
                now = DateTime.Now;

                events = _context.CalendarEventRepository.GetThisMonthsEvents();
            }
            else
            {
                now = new DateTime(year, month, 1);
                events = _context.CalendarEventRepository.GetSpecifiedMonthsEvents(year, month);
            }
            
            if (showOutdatedEvents)
            {
                vm.ShowOutdatedEvents = true;
                vm.AllCalendarEvents = _context.CalendarEventRepository.GetAllEvents().OrderBy(r => r.Date.Ticks).ToList();
            }
            else
            {
                vm.ShowOutdatedEvents = false;
                var currentDate = DateTime.Now;
                vm.AllCalendarEvents = _context.CalendarEventRepository.GetEventsAfterDate(currentDate.Year, currentDate.Month).OrderBy(r => r.Date.Ticks).ToList();
            }
            

            var firstOfMonth = new DateTime(now.Year, now.Month, 1);

            var calendarVM = new CalendarViewModel()
            {
                DaysInMonth = DateTime.DaysInMonth(now.Year, now.Month),
                Month = now.ToString("MMMM", CultureInfo.InvariantCulture),
                MonthNumber = now.Month,
                Year = now.Year,
                InitialOffset = (int)firstOfMonth.DayOfWeek,
                Events = events
            };

            vm.CalendarVM = calendarVM;

            return View(vm);
        }

        public IActionResult ChangeMonth(int changeAmount)
        {
            // Validate is admin
            if (!_credentialHoldingService.IsAdmin) return RedirectToRoute(new { controller = "Home", action = "Index" });

            _calendarHelpingService.ModifyMonth(changeAmount);

            return RedirectToAction("Calendar");
        }

        public IActionResult ResetToToday()
        {
            // Validate is admin
            if (!_credentialHoldingService.IsAdmin) return RedirectToRoute(new { controller = "Home", action = "Index" });
            
            _calendarHelpingService.ResetToToday();

            return RedirectToAction("Calendar");
        }

        public IActionResult ToggleShowOutdated()
        {
            // Validate is admin
            if (!_credentialHoldingService.IsAdmin) return RedirectToRoute(new { controller = "Home", action = "Index" });

            _calendarHelpingService.ShowOutdatedEvents = !_calendarHelpingService.ShowOutdatedEvents;

            return RedirectToAction("Calendar");
        }

        public IActionResult CreateNewEvent()
        {
            // Validate is admin
            if (!_credentialHoldingService.IsAdmin) return RedirectToRoute(new { controller = "Home", action = "Index" });

            var vm = _vmFactory.Resolve<NewEventViewModel>();
            vm.ErrorMessage = "";


            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult NewCalendarEvent(NewEventViewModel vm)
        {
            // Validate is admin
            if (!_credentialHoldingService.IsAdmin) return RedirectToRoute(new { controller = "Home", action = "Index" });

            string errormsg;

            _context.CalendarEventRepository.AddEvent(vm.Date, vm.Title, out errormsg);

            if (errormsg != "")
            {
                var newVm = _vmFactory.Resolve<NewEventViewModel>();
                newVm.ErrorMessage = errormsg;
                return View("CreateNewEvent", newVm);
            }

            return RedirectToAction("Calendar");
        }

        public IActionResult DeleteEvent(int id)
        {
            // Validate is admin
            if (!_credentialHoldingService.IsAdmin) return RedirectToRoute(new { controller = "Home", action = "Index" });

            _context.CalendarEventRepository.DeleteEvent(id);

            return RedirectToAction("Calendar");
        }

        public IActionResult HomeText()
        {
            // Validate is admin
            if (!_credentialHoldingService.IsAdmin) return RedirectToRoute(new { controller = "Home", action = "Index" });

            var vm = _vmFactory.Resolve<HomeTextViewModel>();

            // Populate vm
            vm.Title = _context.HomeTextRepository.GetTitle();
            vm.Body = _context.HomeTextRepository.GetBody();

            return View(vm);            
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult EditHomeText(HomeTextViewModel vm)
        {
            // Validate is admin
            if (!_credentialHoldingService.IsAdmin) return RedirectToRoute(new { controller = "Home", action = "Index" });

            _context.HomeTextRepository.UpdateTitle(vm.Title);
            _context.HomeTextRepository.UpdateBody(vm.Body);

            return RedirectToAction("Index");
        }

        public IActionResult PromoteUser(string searchTerm = ".")
        {
            // Validate is admin
            if (!_credentialHoldingService.IsAdmin) return RedirectToRoute(new { controller = "Home", action = "Index" });

            var vm = _vmFactory.Resolve<PromoteUserViewModel>();

            var allUsers = _context.UserRepository.GetAllUsers();

            var trimmedUsers = _searchUsersService.SearchUsers(allUsers, searchTerm);

            vm.Users = trimmedUsers;
            vm.SearchString = searchTerm;

            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult SearchUser(PromoteUserViewModel vm)
        {
            // Validate is admin
            if (!_credentialHoldingService.IsAdmin) return RedirectToRoute(new { controller = "Home", action = "Index" });

            return RedirectToRoute(new { controller = "Admin", action = "PromoteUser", searchTerm = vm.SearchString });
        }

        public IActionResult ChangeAdminRights(int id, bool admin, string searchTerm)
        {
            // Validate is admin
            if (!_credentialHoldingService.IsAdmin) return RedirectToRoute(new { controller = "Home", action = "Index" });

            _context.UserRepository.ChangeAdminStatus(id, admin);

            return RedirectToRoute(new { controller = "Admin", action = "PromoteUser", searchTerm = searchTerm });
        }
    }
}