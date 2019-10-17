using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpringChickens.Models;
using SpringChickens.ViewModels;

namespace SpringChickens.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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

            vm.Dogs.Add(new Dog()
            {
                PhotoName = @"/images/puppy2.png",
                Title = "Meet Charlie",
                MainBodyText = "This is a really cool dog. His name is Charlie, he is some kind of dog," +
                   " possibly a labrador. He is 3 years old and likes to run around in the field outside his house on the weekends."
            });

            vm.Dogs.Add(new Dog()
            {
                PhotoName = @"/images/newDoggo.png",
                Title = "Fido",
                MainBodyText = "This is Fido, he is also a dog."
            });

            vm.Dogs.Add(new Dog()
            {
                PhotoName = @"/images/notADog.png",
                Title = "Hector",
                MainBodyText = "Hector is not a dog but he is still a good boy. He likes a carrot for breakfast but not for dinner." +
                " Do not feed Hector a carrot at dinner, tea time is fine for carrots, but not dinner. He has 8 brothers and 5 sisters called" +
                " Henry, Tyson, Greg, Hugh, Greg2, Charlie (Not the dog), Dennis, Mac, Henrietta, Hannah, Rhianna, Georgia, Greg(female), Tysonette." +
                " Once more, do not feed Hector a carrot for dinner."
            });

            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
