using Interfaces.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringChickens.ViewModels.FileUpload
{
    public class CreateNewCarouselItemViewModel : IBaseViewModel
    {
        public bool Layout_SignedIn { get; set; }
        public string Layout_Username { get; set; }
        public string Layout_Password { get; set; }
        public bool Layout_IsAdmin { get; set; }


        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile PhotoFile { get; set; }
    }
}
