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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SpringChickens.Controllers
{
    public class FileUploadController : Controller
    {
        private IWebHostEnvironment _env;

        public FileUploadController(IWebHostEnvironment env)
        {
            _env = env;
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

            // TODO: Dependency inject in a db with repo pattern.

            using (var dbContext = new DatabaseContext())
            {
                var firstTripId = dbContext.Trips.First().Id;

                var newPost = new Post()
                {
                    Title = viewmodel.Title,
                    BodyText = viewmodel.Description,
                    PhotoFileName = file.FileName,
                    TripId = firstTripId
                };

                dbContext.Posts.Add(newPost);

                dbContext.SaveChanges();

            }

            

            return RedirectToAction("Index");
        }
    }
}
