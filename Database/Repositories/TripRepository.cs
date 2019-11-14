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
            return Context.Trips.First().Id;
        }
    }
}
