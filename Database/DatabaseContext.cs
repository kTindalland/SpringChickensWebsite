using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Database.Models;

namespace Database
{
    internal class DatabaseContext : DbContext
    {
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ResetToken> ResetTokens { get; set; }
        public DbSet<CarouselItem> CarouselItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=SQL6009.site4now.net;Initial Catalog=DB_A54212_SpringChickens;User Id=DB_A54212_SpringChickens_admin;Password=BAE12345;");
        }


    }
}
