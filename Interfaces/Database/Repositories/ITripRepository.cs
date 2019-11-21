using System;
using System.Collections.Generic;
using System.Text;
using Interfaces.Database.Entities;

namespace Interfaces.Database.Repositories
{
    public interface ITripRepository : IRepository<ITrip>
    {
        // Specific Trip Repository features.

        IEnumerable<ITrip> GetAllTrips();

        string GetTripName(int id);
        int GetFirstTripId();
    }
}
