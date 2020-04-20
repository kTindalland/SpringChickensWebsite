using Interfaces.Database.Entities;
using Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringChickens.ViewModels
{
    public class HomeViewModel : IBaseViewModel
    {
        public bool Layout_SignedIn { get; set; }
        public string Layout_Username { get; set; }
        public string Layout_Password { get; set; }
        public bool Layout_IsAdmin { get; set; }

        public List<ICarouselItem> CarouselItems { get; set; }
    }
}
