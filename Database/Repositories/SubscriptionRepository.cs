using Database.Models;
using Interfaces.Database.Entities;
using Interfaces.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly DbContext _context;

        private DatabaseContext Context => (DatabaseContext)_context;
        public SubscriptionRepository(DbContext context)
        {
            _context = context;
        }

        public bool Add(ISubscription entity)
        {
            Context.Subscriptions.Add((Subscription)entity);
            Context.SaveChanges();
            return true;
        }

        public List<IUser> GetSubscribedUsers(int tripId)
        {
            var subbedUsersIds = Context.Subscriptions.Where(r => r.TripId == tripId).Select(r => r.UserId).ToList<int>();


            var subbedUsers = new List<IUser>();
            foreach(var userId in subbedUsersIds)
            {
                if (Context.Users.Any(r => r.Id == userId))
                {
                    var rec = Context.Users.First(r => r.Id == userId);
                    subbedUsers.Add(rec);
                }
            }

            return subbedUsers;
        }

        public void SubscribeUser(int userId, int tripId)
        {
            var newRec = new Subscription()
            {
                UserId = userId,
                TripId = tripId
            };

            Add(newRec);
        }

        public void UnsubscribeUser(int userId, int tripId)
        {
            if (Context.Subscriptions.Any(r => r.UserId == userId && r.TripId == tripId))
            {
                var rec = Context.Subscriptions.First(r => r.UserId == userId && r.TripId == tripId);
                Context.Subscriptions.Remove(rec);
                Context.SaveChanges();
            }
        }

        public List<int> SubscribedTrips(int userId)
        {
            var subscriptions = Context.Subscriptions.Where(r => r.UserId == userId);

            var tripIds = subscriptions.Select(r => r.TripId).ToList();

            return tripIds;
        }
    }
}
