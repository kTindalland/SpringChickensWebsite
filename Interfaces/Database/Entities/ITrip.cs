using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Database.Entities
{
    public interface ITrip : IEntity
    {
        string TripName { get; set; }

        string TripDescription { get; set; }

        DateTime DateTimeLastActivity { get; set; }
    }
}
