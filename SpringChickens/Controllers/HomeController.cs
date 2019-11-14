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

namespace SpringChickens.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _context;

        public HomeController(IWebHostEnvironment env, IUserService userService, IUnitOfWork context)
        {
            _env = env;
            _userService = userService;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Kai()
        {
            var vm = new DogsViewModel();


            var posts = _context.PostRepository.GetAllPosts();

            foreach (var post in posts)
            {
                var newDog = new Dog()
                {
                    Title = post.Title,
                    MainBodyText = post.BodyText,
                    PhotoName = $"/images/{post.PhotoFileName}",
                    PostId = post.Id
                };

                vm.Dogs.Add(newDog);
            }


            return View(vm);
        }

        public IActionResult Signup()
        {

            var viewmodel = new SignupViewmodel()
            {
                ErrorMessage = "Hello world"
            };

            return View(viewmodel);
        }

        public IActionResult RegisterNewUser(SignupViewmodel viewmodel)
        {
            

            return View("Signup", viewmodel);
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult AllFeeds()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

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
    }
}
