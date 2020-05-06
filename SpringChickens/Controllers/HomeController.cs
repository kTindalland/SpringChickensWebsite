using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpringChickens.Models;
using SpringChickens.ViewModels;
using Interfaces;
using Database;
using Database.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Interfaces.Services;
using Interfaces.Database;
using Interfaces.Database.Entities;
using Interfaces.Factories;
using Interfaces.ViewModels;
using SpringChickens.ViewModels.Admin;
using SpringChickens.ViewModels.Partials;
using System.Globalization;

namespace SpringChickens.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _context;
        private readonly ICredentialHoldingService _credentialHoldingService;
        private readonly IViewModelFactory _viewModelFactory;
        private readonly IPasswordResetService _passwordResetService;
        private readonly ICalendarHelpingService _calendarHelpingService;
        private readonly IEmailService _emailService;

        public HomeController(
            IWebHostEnvironment env,
            IUserService userService,
            IUnitOfWork context,
            ICredentialHoldingService credentialHoldingService,
            IViewModelFactory viewModelFactory,
            IPasswordResetService passwordResetService,
            ICalendarHelpingService calendarHelpingService,
            IEmailService emailService)
        {
            _env = env;
            _userService = userService;
            _context = context;
            _credentialHoldingService = credentialHoldingService;
            _viewModelFactory = viewModelFactory;
            _passwordResetService = passwordResetService;
            _calendarHelpingService = calendarHelpingService;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            var vm = _viewModelFactory.Resolve<HomeViewModel>();

            vm.CarouselItems = _context.CarouselItemRepository.GetAllActiveItems();
            vm.HomeTextTitle = _context.HomeTextRepository.GetTitle();
            vm.HomeTextBody = _context.HomeTextRepository.GetBody();

            return View(vm);
        }

        public IActionResult Privacy()
        {
            var vm = _viewModelFactory.Resolve<BaseViewModel>();

            return View(vm);
        }


        public IActionResult Signup()
        {

            var viewmodel = _viewModelFactory.Resolve<SignupViewmodel>();

            viewmodel.ErrorMessage = "";

            return View(viewmodel);
        }

        public IActionResult Login(BaseViewModel viewmodel)
        {
            var authenticated = _userService.AuthenticateLogin(viewmodel.Layout_Username, viewmodel.Layout_Password);

            if (authenticated)
            {
                IUser user;
                _context.UserRepository.GetUserIfExists(viewmodel.Layout_Username, out user);

                _credentialHoldingService.PopulateService(user);
            }

            return RedirectToAction("Index");
        }

        public IActionResult SignOut()
        {

            _credentialHoldingService.WipeService();

            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult RegisterNewUser(SignupViewmodel viewmodel)
        {
            if (viewmodel.Password != viewmodel.ConfirmPassword)
            {
                viewmodel.UserName = "";
                viewmodel.Email = "";
                viewmodel.Password = "";
                viewmodel.ConfirmPassword = "";

                viewmodel.ErrorMessage = "Passwords do not match.";

                return View("Signup", viewmodel);
            }

            string errormsg;
            if (!_userService.CreateNewUser(viewmodel.UserName, viewmodel.Password, viewmodel.Email, out errormsg))
            {
                viewmodel.UserName = "";
                viewmodel.Email = "";
                viewmodel.Password = "";
                viewmodel.ConfirmPassword = "";

                viewmodel.ErrorMessage = errormsg;

                return View("Signup", viewmodel);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Contact()
        {
            var vm = _viewModelFactory.Resolve<BaseViewModel>();

            return View(vm);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var vm = _viewModelFactory.Resolve<ErrorViewModel>();
            vm.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            return View(vm);
        }

        // TODO: Needs to be removed at some point.
        public IActionResult DeletePost(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Kai");



            IPost post;

            if (_context.PostRepository.GetByIdIfExists(id, out post))
            {
                    

                System.IO.File.Delete($"{ _env.WebRootPath}/images/{post.PhotoFileName}");

                _context.PostRepository.RemovePost(post);

                _context.SaveChanges();
            }

            return RedirectToAction("Kai");
        }

        public IActionResult Calendar()
        {
            var year = _calendarHelpingService.Year;
            var month = _calendarHelpingService.Month;
            var showOutdatedEvents = _calendarHelpingService.ShowOutdatedEvents;

            var vm = _viewModelFactory.Resolve<CalendarManagementViewModel>();

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
            _calendarHelpingService.ModifyMonth(changeAmount);

            return RedirectToAction("Calendar");
        }

        public IActionResult ResetToToday()
        {
            _calendarHelpingService.ResetToToday();

            return RedirectToAction("Calendar");
        }

        public IActionResult ToggleShowOutdated()
        {
            _calendarHelpingService.ShowOutdatedEvents = !_calendarHelpingService.ShowOutdatedEvents;

            return RedirectToAction("Calendar");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Contact(HomeViewModel vm)
        {
            _emailService.SendContactMessage(vm.ContactName, vm.ContactEmail, vm.ContactMessage);

            return RedirectToAction("Index");
        }
    }
}
