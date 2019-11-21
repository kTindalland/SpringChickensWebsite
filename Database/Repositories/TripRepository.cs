using System;
using System.Collections.Generic;
using System.Text;
using Interfaces.Database.Entities;
using Interfaces.Database.Repositories;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Database.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly DbContext _context;

        private DatabaseContext Context { get => (DatabaseContext)_context; }

        public TripRepository(DbContext context)
        {
            _context = context;
        }

        public bool Add(ITrip entity)
        {
            Context.Add((Trip)entity);

            return true;
        }

        public int GetFirstTripId()
        {
            var result = Context.Trips.First().Id;
            return result;
        }

        public IEnumerable<ITrip> GetAllTrips()
        {
            var result = Context.Trips.ToList();
            return result;
        }

        public string GetTripName(int id)
        {
            string result = "Erroneous Trip Name";

            if (Context.Trips.Any(r => r.Id == id))
            {
                result = Context.Trips.First(r => r.Id == id).TripName;
            }

            return result;
        }
    }
}
