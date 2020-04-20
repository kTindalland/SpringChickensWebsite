using System;
using System.Collections.Generic;
using System.Text;
using Interfaces.Database;
using Interfaces.Database.Repositories;
using Database.Repositories;

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

        public DatabaseUnitOfWork()
        {
            _context = new DatabaseContext();

            UserRepository = new UserRepository(_context);
            TripRepository = new TripRepository(_context);
            PostRepository = new PostRepository(_context);
            ResetTokenRepository = new ResetTokenRepository(_context);
            CarouselItemRepository = new CarouselItemRepository(_context);
        }

        public bool SaveChanges()
        {
            _context.SaveChanges();

            return true;
        }
    }
}
