using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpringChickens.Models;
using Interfaces.ViewModels;

namespace SpringChickens.ViewModels
{
    public class DogsViewModel : IBaseViewModel
    {
        public List<Dog> Dogs { get; set; }
        public bool Layout_SignedIn { get; set; }
        public string Layout_Username { get; set; }
        public string Layout_Password { get; set; }
        public bool Layout_IsAdmin { get; set; }

        public DogsViewModel()
        {
            Dogs = new List<Dog>();
        }
    }
}
