using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.ViewModels;

namespace SpringChickens.Models
{
    public class FileUploadViewModel : IBaseViewModel
    {

        public IFormFile File { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        public bool Layout_SignedIn { get; set; }
        public string Layout_Username { get; set; }
        public string Layout_Password { get; set; }
    }
}
