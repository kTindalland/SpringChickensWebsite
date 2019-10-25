using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Database.Models;

namespace Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=SQL6007.site4now.net;Initial Catalog=DB_A4EE1C_SpringChickens;User Id=DB_A4EE1C_SpringChickens_admin;Password=BAE12345;");
        }


    }
}
