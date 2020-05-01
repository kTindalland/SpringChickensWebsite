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
    class HomeTextRepository : IHomeTextRepository
    {
        private readonly DbContext _context;

        private DatabaseContext Context => (DatabaseContext)_context;
        public HomeTextRepository(DbContext context)
        {
            _context = context;
        }

        public bool Add(IHomeText entity)
        {
            Context.HomeTexts.Add((HomeText)entity);
            Context.SaveChanges();
            return true;
        }

        private IHomeText GetRecord()
        {
            var rec = Context.HomeTexts.First();

            return rec;
        }

        public string GetTitle()
        {
            var rec = GetRecord();

            return rec.Title;
        }

        public string GetBody()
        {
            var rec = GetRecord();

            return rec.Body;
        }

        public void UpdateTitle(string newTitle)
        {
            var rec = GetRecord();

            rec.Title = newTitle;

            Context.SaveChanges();
        }

        public void UpdateBody(string newBody)
        {
            var rec = GetRecord();

            rec.Body = newBody;

            Context.SaveChanges();
        }
    }
}
