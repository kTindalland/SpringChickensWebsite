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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SpringChickens.Controllers
{
    public class FileUploadController : Controller
    {
        private IWebHostEnvironment _env;
        private IUnitOfWork _context;

        public FileUploadController(IWebHostEnvironment env, IUnitOfWork context)
        {
            _env = env;
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult SingleFile(FileUploadViewModel viewmodel)
        {
            var dir = _env.WebRootPath;

            var file = viewmodel.File;
            // Add name validation.


            using (var fileStream = new FileStream($"{dir}/images/{viewmodel.File.FileName}", FileMode.Create, FileAccess.ReadWrite))
            {
                file.CopyTo(fileStream);
            }


            var firstTripId = _context.TripRepository.GetFirstTripId();

            _context.PostRepository.CreateAndAddNewPost(viewmodel.Title, viewmodel.Description, file.FileName, firstTripId);


            _context.SaveChanges();

            

            return RedirectToAction("Index");
        }
    }
}
