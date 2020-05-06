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

        public string HomeTextTitle { get; set; }
        public string HomeTextBody { get; set; }

        public string ContactEmail { get; set; }
        public string ContactName { get; set; }
        public string ContactMessage { get; set; }
    }
}
