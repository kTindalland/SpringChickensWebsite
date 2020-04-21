using Interfaces.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Database.Repositories
{
    public interface ICalendarEventRepository : IRepository<ICalendarEvent>
    {
        List<ICalendarEvent> GetEventsAfterDate(int year, int month); // Output includes the date given
        List<ICalendarEvent> GetThisMonthsEvents();
        List<ICalendarEvent> GetSpecifiedMonthsEvents(int year, int month);
        List<ICalendarEvent> GetAllEvents();
        void DeleteEvent(int id);
        void AddEvent(DateTime date, string description);
    }
}
