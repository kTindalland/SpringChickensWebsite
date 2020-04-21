using Interfaces.Database.Entities;
using Interfaces.ViewModels;
using SpringChickens.ViewModels.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringChickens.ViewModels.Admin
{
    public class CalendarManagementViewModel : IBaseViewModel
    {
        public bool Layout_SignedIn { get; set; }
        public string Layout_Username { get; set; }
        public string Layout_Password { get; set; }
        public bool Layout_IsAdmin { get; set; }

        public CalendarViewModel CalendarVM {get; set;}
        public List<ICalendarEvent> AllCalendarEvents { get; set; }
        public bool ShowOutdatedEvents { get; set; }
    }
}
