using Interfaces.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models
{
    public class CarouselItem : ICarouselItem
    {
        public string PhotoFilePath { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int Position { get; set; }
        public int Id { get; set; }
    }
}
