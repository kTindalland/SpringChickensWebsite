using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpringChickens.Models;

namespace SpringChickens.ViewModels
{
    public class DogsViewModel
    {
        public List<Dog> Dogs { get; set; }

        public DogsViewModel()
        {
            Dogs = new List<Dog>();
        }
    }
}
