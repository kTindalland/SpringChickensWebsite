using Interfaces.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Database.Repositories
{
    public interface ISubscriptionRepository : IRepository<ISubscription>
    {
        List<IUser> GetSubscribedUsers(int tripId);
        void SubscribeUser(int userId, int tripId);
        void UnsubscribeUser(int userId, int tripId);
        List<int> SubscribedTrips(int userId);
    }
}
