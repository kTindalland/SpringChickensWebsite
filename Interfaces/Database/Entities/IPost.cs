using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.Database.Entities
{
    public interface IPost : IEntity
    {
        int TripId { get; set; }

        string PhotoFileName { get; set; }

        string Title { get; set; }

        string BodyText { get; set; }
    }
}
