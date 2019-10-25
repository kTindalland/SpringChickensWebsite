using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Interfaces.Database;

namespace Database.Models
{
    public class Trip : IEntity
    {
        public int Id { get; set; }

        public string TripName { get; set; }

        public string TripDescription { get; set; }
    }
}
