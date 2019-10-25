using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringChickens.Models
{
    public class FileUploadViewModel
    {

        public IFormFile File { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

    }
}
