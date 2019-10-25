using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Interfaces.Database;
using System.ComponentModel.DataAnnotations;

namespace Database.Models
{
    public class Post : IEntity
    {
        public int Id { get; set; }

        public int TripId { get; set; }

        public string PhotoFileName { get; set; }

        public string Title { get; set; }

        public string BodyText { get; set; }

    }
}
