using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Interfaces.Factories;
using SpringChickens.ViewModels;
using Interfaces.Database;
using Microsoft.AspNetCore.Hosting;
using Interfaces.Database.Entities;
using Microsoft.AspNetCore.Routing;
using Interfaces.Services;

namespace SpringChickens.Controllers
{
    public class TripController : Controller
    {
        private readonly IViewModelFactory _viewModelFactory;
        private readonly IUnitOfWork _context;
        private readonly IWebHostEnvironment _env;
        private readonly ICredentialHoldingService _credentialHoldingService;

        public TripController(
            IViewModelFactory viewModelFactory,
            IUnitOfWork context,
            IWebHostEnvironment env,
            ICredentialHoldingService credentialHoldingService)
        {
            _viewModelFactory = viewModelFactory;
            _context = context;
            _env = env;
            _credentialHoldingService = credentialHoldingService;
        }

        public IActionResult Index()
        {
            var vm = _viewModelFactory.Resolve<AllTripsViewModel>();

            vm.Trips = _context.TripRepository.GetAllTrips().ToList();

            if (_credentialHoldingService.LoggedIn)
            {
                IUser user;
                _context.UserRepository.GetUserIfExists(_credentialHoldingService.Username, out user);

                vm.SubscribedTrips = _context.SubscriptionRepository.SubscribedTrips(user.Id);
            }

            return View(vm);
        }

        // NEED TO UPDATE TO ADD IMAGES PATH LIKE IN KAI IN HOME CONT.
        public IActionResult ViewTrip(int id)
        {
            var vm = _viewModelFactory.Resolve<TripViewModel>();

            vm.Posts = _context.PostRepository.GetAllPostsFromTrip(id).ToList();

            vm.TripName = _context.TripRepository.GetTripName(id);

            vm.Delete_TripId = id;

            vm.PathPrefix = "../../images/";

            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DeletePost(TripViewModel vm, string tripId)
        {
            if (!_credentialHoldingService.IsAdmin) return RedirectToRoute(new { controller = "Home", action = "Index" });

            if (vm.Delete_Id == 0)
                return RedirectToAction("Index");

            IPost post;

            if (_context.PostRepository.GetByIdIfExists(vm.Delete_Id, out post))
            {


                System.IO.File.Delete($"{ _env.WebRootPath}/images/{post.PhotoFileName}");

                _context.PostRepository.RemovePost(post);

                _context.SaveChanges();

                _context.TripRepository.ResetTimeOnTrip(int.Parse(tripId));
            }
                      

            return RedirectToAction("ViewTrip", "Trip", new RouteValueDictionary() { {"id", tripId} });
        }

        public IActionResult DeleteTrip(int id)
        {
            // Validate is admin
            if (!_credentialHoldingService.IsAdmin) return RedirectToRoute(new { controller = "Home", action = "Index" });

            _context.TripRepository.DeleteTrip(id);

            //TODO: Cleanup orphaned files at this point

            return RedirectToAction("ViewTrip");
        }

        public IActionResult Subscribe(int tripId)
        {
            if (_credentialHoldingService.LoggedIn)
            {
                IUser user;
                _context.UserRepository.GetUserIfExists(_credentialHoldingService.Username, out user);

                _context.SubscriptionRepository.SubscribeUser(user.Id, tripId);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Unsubscribe(int tripId)
        {
            if (_credentialHoldingService.LoggedIn)
            {
                IUser user;
                _context.UserRepository.GetUserIfExists(_credentialHoldingService.Username, out user);

                _context.SubscriptionRepository.UnsubscribeUser(user.Id, tripId);
            }

            return RedirectToAction("Index");
        }
    }
}