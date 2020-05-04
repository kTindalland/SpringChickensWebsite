using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.ViewModels;
using Interfaces.Database.Entities;

namespace SpringChickens.ViewModels
{
    public class AllTripsViewModel : IBaseViewModel
    {
        public List<ITrip> Trips { get; set; }
        public List<int> SubscribedTrips { get; set; }
        public bool Layout_SignedIn { get; set; }
        public string Layout_Username { get; set; }
        public string Layout_Password { get; set; }
        public bool Layout_IsAdmin { get; set; }
    }
}
