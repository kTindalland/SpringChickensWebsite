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

namespace SpringChickens.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDatabaseService _dbService;
        private readonly IHostingEnvironment _env;

        public HomeController(ILogger<HomeController> logger, IDatabaseService dbService, IHostingEnvironment env)
        {
            _logger = logger;
            _dbService = dbService;
            _env = env;
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


            using (var dbContext = new DatabaseContext())
            {
                var posts = dbContext.Posts.ToList();

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
            }


            //vm.Dogs.Add(new Dog()
            //{
            //    PhotoName = @"/images/puppy2.png",
            //    Title = "Meet Charlie",
            //    MainBodyText = "This is a really cool dog. His name is Charlie, he is some kind of dog," +
            //       " possibly a labrador. He is 3 years old and likes to run around in the field outside his house on the weekends."
            //});

            //vm.Dogs.Add(new Dog()
            //{
            //    PhotoName = @"/images/newDoggo.png",
            //    Title = "Fido",
            //    MainBodyText = "This is Fido, he is also a dog."
            //});

            //vm.Dogs.Add(new Dog()
            //{
            //    PhotoName = @"/images/notADog.png",
            //    Title = "Hector",
            //    MainBodyText = "Hector is not a dog but he is still a good boy. He likes a carrot for breakfast but not for dinner." +
            //    " Do not feed Hector a carrot at dinner, tea time is fine for carrots, but not dinner. He has 8 brothers and 5 sisters called" +
            //    " Henry, Tyson, Greg, Hugh, Greg2, Charlie (Not the dog), Dennis, Mac, Henrietta, Hannah, Rhianna, Georgia, Greg(female), Tysonette." +
            //    " Once more, do not feed Hector a carrot for dinner."
            //});

            return View(vm);
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


            using (var dbContext = new DatabaseContext())
            {
                var isId = dbContext.Posts.Any(r => r.Id == id);

                if (isId)
                {
                    var post = dbContext.Posts.First(r => r.Id == id);

                    System.IO.File.Delete($"{ _env.WebRootPath}/images/{post.PhotoFileName}");

                    dbContext.Posts.Remove(post);

                    dbContext.SaveChanges();
                }
            }

            return RedirectToAction("Kai");
        }
    }
}
