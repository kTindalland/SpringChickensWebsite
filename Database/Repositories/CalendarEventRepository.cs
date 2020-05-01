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
    public class CalendarEventRepository : ICalendarEventRepository
    {
        private readonly DbContext _context;
        private DatabaseContext Context => (DatabaseContext)_context;

        public CalendarEventRepository(DbContext context)
        {
            _context = context;
        }

        public bool Add(ICalendarEvent entity)
        {
            Context.CalendarEvents.Add((CalendarEvent)entity);
            Context.SaveChanges();
            return true;
        }

        public void AddEvent(DateTime date, string description, out string errormessage)
        {
            var newRecord = new CalendarEvent()
            {
                Date = date,
                Description = description
            };

            if (Context.CalendarEvents.Any(r => r.Date.Year == newRecord.Date.Year && r.Date.Month == newRecord.Date.Month && r.Date.Day == newRecord.Date.Day))
            {
                errormessage = "There is already an event on this day.";
                return;
            }

            errormessage = "";
            Add(newRecord);
        }

        public void DeleteEvent(int id)
        {
            // Validate it exists
            if (!Context.CalendarEvents.Any(r => r.Id == id))
            {
                return;
            }

            // Get the record
            var record = Context.CalendarEvents.First(r => r.Id == id);
            Context.CalendarEvents.Remove(record);
            Context.SaveChanges();
        }

        public List<ICalendarEvent> GetAllEvents()
        {
            var result = Context.CalendarEvents.ToList<ICalendarEvent>();

            return result;
        }

        public List<ICalendarEvent> GetSpecifiedMonthsEvents(int year, int month)
        {
            var result = Context.CalendarEvents.Where(r => r.Date.Year == year && r.Date.Month == month).ToList<ICalendarEvent>();

            return result;
        }

        public List<ICalendarEvent> GetThisMonthsEvents()
        {
            var now = DateTime.Now;

            var result = Context.CalendarEvents.Where(r => r.Date.Year == now.Year && r.Date.Month == now.Month).ToList<ICalendarEvent>();

            return result;
        }

        public List<ICalendarEvent> GetEventsAfterDate(int year, int month)
        {
            var result = Context.CalendarEvents.Where(r => r.Date.Year >= year && r.Date.Month >= month).ToList<ICalendarEvent>();

            return result;
        }
    }
}
