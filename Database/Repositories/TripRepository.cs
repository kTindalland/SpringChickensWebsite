using System;
using System.Collections.Generic;
using System.Text;
using Interfaces.Database.Entities;
using Interfaces.Database.Repositories;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.EntityFrameworkCore.Update;

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
            Context.SaveChanges();
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

        public ITrip GetTripFromId(int id)
        {
            return Context.Trips.First(r => r.Id == id);
        }

        public void ResetTimeOnTrip(int id)
        {
            var tripPosts = Context.Posts.Where(r => r.TripId == id).OrderByDescending(r => r.DateTimePosted).ToList();

            if (tripPosts.Count > 0)
            {
                // get trip
                var trip = Context.Trips.First(r => r.Id == id);

                trip.DateTimeLastActivity = tripPosts.First().DateTimePosted;

                Context.SaveChanges();
            }
        }

        public void CreateTrip(string name, string description)
        {
            var newTrip = new Trip()
            {
                TripName = name,
                TripDescription = description,
                DateTimeLastActivity = DateTime.Now
            };

            Add(newTrip);
        }

        public void DeleteTrip(int id)
        {
            if (Context.Trips.Any(r => r.Id == id))
            {
                var rec = Context.Trips.First(r => r.Id == id);

                var posts = Context.Posts.Where(r => r.TripId == id);

                Context.Trips.Remove(rec);
                Context.Posts.RemoveRange(posts);

                Context.SaveChanges();
            }
        }

        public List<string> TripFilenames(int id)
        {
            var result = new List<string>();

            if (Context.Trips.Any(r => r.Id == id))
            {
                var rec = Context.Trips.First(r => r.Id == id);

                var posts = Context.Posts.Where(r => r.TripId == id);

                result = posts.Select(r => r.PhotoFileName).ToList();
            }

            return result;
        }
    }
}
