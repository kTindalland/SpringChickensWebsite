using System;
using System.Collections.Generic;
using System.Text;
using Interfaces.Database;
using Interfaces.Database.Repositories;
using Database.Repositories;
using Microsoft.Extensions.Configuration;

namespace Database
{
    public class DatabaseUnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;

        public IUserRepository UserRepository { get; }
        public ITripRepository TripRepository { get; }
        public IPostRepository PostRepository { get; }
        public IResetTokenRepository ResetTokenRepository { get; }
        public ICarouselItemRepository CarouselItemRepository { get; }
        public ICalendarEventRepository CalendarEventRepository { get; }
        public IHomeTextRepository HomeTextRepository { get; }
        public ISubscriptionRepository SubscriptionRepository { get; }

        public DatabaseUnitOfWork(IConfiguration configuration)
        {
            _context = new DatabaseContext(configuration);

            UserRepository = new UserRepository(_context);
            TripRepository = new TripRepository(_context);
            PostRepository = new PostRepository(_context);
            ResetTokenRepository = new ResetTokenRepository(_context);
            CarouselItemRepository = new CarouselItemRepository(_context);
            CalendarEventRepository = new CalendarEventRepository(_context);
            HomeTextRepository = new HomeTextRepository(_context);
            SubscriptionRepository = new SubscriptionRepository(_context);
        }


        public bool SaveChanges()
        {
            _context.SaveChanges();

            return true;
        }
    }
}
