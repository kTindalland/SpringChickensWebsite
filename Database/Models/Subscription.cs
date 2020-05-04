using Interfaces.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models
{
    public class Subscription : ISubscription
    {
        public int UserId { get; set; }
        public int TripId { get; set; }
        public int Id { get; set; }
    }
}
