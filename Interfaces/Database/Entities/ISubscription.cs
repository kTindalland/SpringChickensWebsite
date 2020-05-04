using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Database.Entities
{
    public interface ISubscription : IEntity
    {
        int UserId { get; set; }
        int TripId { get; set; }
    }
}
