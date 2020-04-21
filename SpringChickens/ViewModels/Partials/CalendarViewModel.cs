using Interfaces.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringChickens.ViewModels.Partials
{
    public class CalendarViewModel
    {

        public int Year { get; set; }
        public string Month { get; set; }
        public int MonthNumber { get; set; }
        public int DaysInMonth { get; set; }
        public int InitialOffset { get; set; } // Amount of blank days before the first of the month. Week starts on a Monday
        public List<ICalendarEvent> Events { get; set; }

    }
}
