﻿using BookSellRazor_temp.Model;
using Microsoft.EntityFrameworkCore;

namespace BookSellRazor_temp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> categories { get; set; }
        public object Categories { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = "1" },
                 new Category { Id = 2, Name = "SciFi", DisplayOrder = "2" },
                 new Category { Id = 3, Name = "History", DisplayOrder = "3" }
                );
        }
    }
}