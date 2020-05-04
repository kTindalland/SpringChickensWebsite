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
        ITrip GetTripFromId(int id);
        void ResetTimeOnTrip(int id); // Gets latest post time
        void CreateTrip(string name, string description);
        void DeleteTrip(int id);
        List<string> TripFilenames(int id);
    }
}
