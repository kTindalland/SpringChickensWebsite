using Database.Models;
using Interfaces.Database.Entities;
using Interfaces.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database.Repositories
{
    public class CarouselItemRepository : ICarouselItemRepository
    {
        private readonly DbContext _context;

        private DatabaseContext Context => (DatabaseContext)_context;

        public CarouselItemRepository(DbContext context)
        {
            _context = context;
        }

        public void Update(ICarouselItem item)
        {
            Context.CarouselItems.Update((CarouselItem)item);
            Context.SaveChangesAsync();
        }

        public bool Add(ICarouselItem entity)
        {
            Context.CarouselItems.Add((CarouselItem)entity);
            return true;
        }

        private void ChangeActivation(int id, bool value)
        {
            if (Context.CarouselItems.Any(r => r.Id == id))
            {
                var record = Context.CarouselItems.First(r => r.Id == id);

                record.IsActive = value;
                Context.SaveChangesAsync();
            }
        }

        public void ActivateItem(int id)
        {
            ChangeActivation(id, true);
        }

        public void DeactivateItem(int id)
        {
            ChangeActivation(id, false);
        }

        public List<ICarouselItem> GetAllItems()
        {
            var result = Context.CarouselItems.Where(r => true).OrderBy(r => r.Position).ToList<ICarouselItem>();

            return result;
        }

        public List<ICarouselItem> GetAllActiveItems()
        {
            var result = Context.CarouselItems.Where(r => r.IsActive).OrderBy(r => r.Position).ToList<ICarouselItem>();

            return result;
        }
    }
}
