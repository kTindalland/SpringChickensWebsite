using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Interfaces.Database.Entities;

namespace Database.Models
{
    internal class Trip : ITrip
    {
        public int Id { get; set; }

        public string TripName { get; set; }

        public string TripDescription { get; set; }
    }
}
