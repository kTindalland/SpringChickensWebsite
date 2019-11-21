using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Interfaces.Factories;
using SpringChickens.ViewModels;
using Interfaces.Database;

namespace SpringChickens.Controllers
{
    public class TripController : Controller
    {
        private readonly IViewModelFactory _viewModelFactory;
        private readonly IUnitOfWork _context;

        public TripController(
            IViewModelFactory viewModelFactory,
            IUnitOfWork context)
        {
            _viewModelFactory = viewModelFactory;
            _context = context;
        }

        public IActionResult Index()
        {
            var vm = _viewModelFactory.Resolve<AllTripsViewModel>();

            vm.Trips = _context.TripRepository.GetAllTrips().ToList();

            return View(vm);
        }

        public IActionResult ViewTrip(int id)
        {
            var vm = _viewModelFactory.Resolve<TripViewModel>();

            vm.Posts = _context.PostRepository.GetAllPostsFromTrip(id).ToList();

            vm.TripName = _context.TripRepository.GetTripName(id);

            return View(vm);
        }
    }
}