using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Services
{
    public interface ICalendarHelpingService
    {
        int Year { get; }
        int Month { get; }
        bool ShowOutdatedEvents { get; set; }
        void ModifyMonth(int change);
        void ResetToToday();

    }
}
