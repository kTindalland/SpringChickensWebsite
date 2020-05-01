using Interfaces.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models
{
    public class HomeText : IHomeText
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public int Id { get; set; }
    }
}
