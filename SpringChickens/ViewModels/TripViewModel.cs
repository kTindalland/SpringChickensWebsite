using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.ViewModels;
using Interfaces.Database.Entities;

namespace SpringChickens.ViewModels
{
    public class TripViewModel : IBaseViewModel
    {
        public bool Layout_SignedIn { get; set; }
        public string Layout_Username { get; set; }
        public string Layout_Password { get; set; }

        public List<IPost> Posts { get; set; }

        public string TripName { get; set; }
    }
}
