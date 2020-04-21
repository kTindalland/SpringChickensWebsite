using Interfaces.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models
{
    public class CalendarEvent : ICalendarEvent
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
    }
}
