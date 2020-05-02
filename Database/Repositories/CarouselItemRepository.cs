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
            Context.SaveChanges();
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
                Context.SaveChanges();
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

        public void FlipActivation(int id)
        {
            // validate item exists
            if (!Context.CarouselItems.Any(r => r.Id == id))
            {
                return;
            }

            // get item
            var record = Context.CarouselItems.First(r => r.Id == id);

            record.IsActive = !record.IsActive;
            Context.SaveChanges();
        }

        public void MoveItemUp(int id)
        {
            // validate item exists
            if (!Context.CarouselItems.Any(r => r.Id == id))
            {
                return;
            }

            // get item
            var record = Context.CarouselItems.First(r => r.Id == id);

            // Check there's space above it
            if ((record.Position - 1) <= 0)
            {
                return;
            }

            // Get record above current
            var aboveRecord = Context.CarouselItems.First(r => r.Position == record.Position - 1);

            record.Position--;
            aboveRecord.Position++;

            Context.SaveChanges();
        }

        public void MoveItemDown(int id)
        {
            // validate item exists
            if (!Context.CarouselItems.Any(r => r.Id == id))
            {
                return;
            }

            // get item
            var record = Context.CarouselItems.First(r => r.Id == id);

            // Check there's space below it
            if ((record.Position + 1) > Context.CarouselItems.Count())
            {
                return;
            }

            // Get record below current
            var belowRecord = Context.CarouselItems.First(r => r.Position == record.Position + 1);

            record.Position++;
            belowRecord.Position--;

            Context.SaveChanges();
        }

        public void CreateNewCarouselItem(string title, string description, string photoFileName)
        {
            // Get the last position
            var lastPosition = Context.CarouselItems.Count() + 1;

            // Create the record
            var record = new CarouselItem()
            {
                Title = title,
                Description = description,
                PhotoFilePath = photoFileName,
                IsActive = true,
                Position = lastPosition
            };

            Context.CarouselItems.Add(record);
            Context.SaveChanges();
        }

        public void DeleteItem(int id)
        {
            // get the record if it exists
            if (!Context.CarouselItems.Any(r =>r.Id == id))
            {
                return;
            }

            var record = Context.CarouselItems.First(r => r.Id == id);

            // Move up all the other records

            var allRecordsBelow = Context.CarouselItems.Where(r => r.Position > record.Position);

            foreach(var item in allRecordsBelow)
            {
                item.Position--;
            }

            Context.Remove(record);

            Context.SaveChanges();
        }

        public string GetPhotoFileName(int id)
        {
            // get the record if it exists
            if (!Context.CarouselItems.Any(r => r.Id == id))
            {
                return "RecordNonExistant";
            }

            var record = Context.CarouselItems.First(r => r.Id == id);

            return record.PhotoFilePath;
        }
    }
}
