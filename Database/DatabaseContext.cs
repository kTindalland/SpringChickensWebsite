using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Database.Models;
using Microsoft.Extensions.Configuration;

namespace Database
{
    internal class DatabaseContext : DbContext
    {
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ResetToken> ResetTokens { get; set; }
        public DbSet<CarouselItem> CarouselItems { get; set; }
        public DbSet<CalendarEvent> CalendarEvents { get; set; }
        public DbSet<HomeText> HomeTexts { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        private string _connectionString;

        public DatabaseContext(IConfiguration configuration)
        {
            _connectionString = configuration["DefaultConnection"];
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }


    }
}
