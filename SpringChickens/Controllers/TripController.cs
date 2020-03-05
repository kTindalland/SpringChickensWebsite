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

namespace SpringChickens.Controllers
{
    public class TripController : Controller
    {
        private readonly IViewModelFactory _viewModelFactory;
        private readonly IUnitOfWork _context;
        private readonly IWebHostEnvironment _env;

        public TripController(
            IViewModelFactory viewModelFactory,
            IUnitOfWork context,
            IWebHostEnvironment env)
        {
            _viewModelFactory = viewModelFactory;
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            var vm = _viewModelFactory.Resolve<AllTripsViewModel>();

            vm.Trips = _context.TripRepository.GetAllTrips().ToList();

            return View(vm);
        }

        // NEED TO UPDATE TO ADD IMAGES PATH LIKE IN KAI IN HOME CONT.
        public IActionResult ViewTrip(int id)
        {
            var vm = _viewModelFactory.Resolve<TripViewModel>();

            vm.Posts = _context.PostRepository.GetAllPostsFromTrip(id).ToList();

            vm.TripName = _context.TripRepository.GetTripName(id);

            vm.Delete_TripId = id;

            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DeletePost(TripViewModel vm)
        {
            if (vm.Delete_Id == 0)
                return RedirectToAction("Index");

            IPost post;

            if (_context.PostRepository.GetByIdIfExists(vm.Delete_Id, out post))
            {


                System.IO.File.Delete($"{ _env.WebRootPath}/images/{post.PhotoFileName}");

                _context.PostRepository.RemovePost(post);

                _context.SaveChanges();
            }

            return RedirectToAction("ViewTrip", vm.Delete_TripId);
        }
    }
}