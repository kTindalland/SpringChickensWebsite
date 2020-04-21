using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Database.Entities
{
    public interface ICalendarEvent : IEntity
    {
        DateTime Date { get; set; }
        string Description { get; set; }
    }
}
