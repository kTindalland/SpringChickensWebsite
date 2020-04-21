using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using SpringChickens.Models;
using Database.Models;
using Database;
using System.Web;
using Interfaces.Database;
using Interfaces.Database.Entities;
using Interfaces.Factories;
using SpringChickens.ViewModels;
using Interfaces.Services;
using SpringChickens.ViewModels.FileUpload;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SpringChickens.Controllers
{
    public class FileUploadController : Controller
    {
        private IWebHostEnvironment _env;
        private IUnitOfWork _context;
        private readonly IViewModelFactory _viewModelFactory;
        private readonly ICredentialHoldingService _credentialHoldingService;

        public FileUploadController(
            IWebHostEnvironment env,
            IUnitOfWork context,
            IViewModelFactory viewModelFactory,
            ICredentialHoldingService credentialHoldingService)
        {
            _env = env;
            _context = context;
            _viewModelFactory = viewModelFactory;
            _credentialHoldingService = credentialHoldingService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            // Validate is admin
            if (!_credentialHoldingService.IsAdmin) return RedirectToRoute(new { controller = "Home", action = "Index" });


            var vm = _viewModelFactory.Resolve<FileUploadViewModel>();

            var trips = _context.TripRepository.GetAllTrips().ToList();

            Dictionary<int, string> tripsDict = new Dictionary<int, string>();

            foreach (var trip in trips.OrderBy(t => t.Id)) {
                tripsDict[trip.Id] = trip.TripName;
            }

            vm.Trips = tripsDict;

            return View(vm);
        }

        public IActionResult AddNewTrip()
        {
            // Validate is admin
            if (!_credentialHoldingService.IsAdmin) return RedirectToRoute(new { controller = "Home", action = "Index" });

            var vm = _viewModelFactory.Resolve<AddNewTripViewModel>();

            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult SingleFile(FileUploadViewModel viewmodel)
        {
            // Validate is admin
            if (!_credentialHoldingService.IsAdmin) return RedirectToRoute(new { controller = "Home", action = "Index" });

            var dir = _env.WebRootPath;

            var file = viewmodel.File;
            var filename = DateTime.Now.Ticks.ToString() + file.FileName;


            using (var fileStream = new FileStream($"{dir}/images/{filename}", FileMode.Create, FileAccess.ReadWrite))
            {
                file.CopyTo(fileStream);
            }


            var firstTripId = _context.TripRepository.GetFirstTripId();

            _context.PostRepository.CreateAndAddNewPost(viewmodel.Title, viewmodel.Description, filename, firstTripId);


            _context.SaveChanges();

            

            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CreateTrip(AddNewTripViewModel vm)
        {
            // Validate is admin
            if (!_credentialHoldingService.IsAdmin) return RedirectToRoute(new { controller = "Home", action = "Index" });

            return RedirectToAction("AddNewTrip");
        }

        public IActionResult UploadCarouselItem()
        {
            // Validate is admin
            if (!_credentialHoldingService.IsAdmin) return RedirectToRoute(new { controller = "Home", action = "Index" });

            var vm = _viewModelFactory.Resolve<CreateNewCarouselItemViewModel>();

            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult UploadCarouselItem(CreateNewCarouselItemViewModel viewmodel)
        {
            // Validate is admin
            if (!_credentialHoldingService.IsAdmin) return RedirectToRoute(new { controller = "Home", action = "Index" });

            var dir = _env.WebRootPath;

            var file = viewmodel.PhotoFile;
            var filename = DateTime.Now.Ticks.ToString()+file.FileName;


            using (var fileStream = new FileStream($"{dir}/carouselImages/{filename}", FileMode.Create, FileAccess.ReadWrite))
            {
                file.CopyTo(fileStream);
            }

            // Create the carousel item record
            _context.CarouselItemRepository.CreateNewCarouselItem(viewmodel.Title, viewmodel.Description, filename);

            return RedirectToRoute(new { controller = "Admin", action = "CarouselManagement" });
        }
    }
}
