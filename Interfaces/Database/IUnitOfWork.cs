using System;
using System.Collections.Generic;
using System.Text;
using Interfaces.Database.Repositories;

namespace Interfaces.Database
{
    public interface IUnitOfWork
    {
        bool SaveChanges();

        IUserRepository UserRepository { get; }
        ITripRepository TripRepository { get; }
        IPostRepository PostRepository { get; }
        IResetTokenRepository ResetTokenRepository { get; }
        ICarouselItemRepository CarouselItemRepository { get; }
        ICalendarEventRepository CalendarEventRepository { get; }
    }
}
