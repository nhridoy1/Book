using BookSell.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BookSell.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public  DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                 new Category { Id = 1, Name = "Action", DisplayOrder = "1"},
                 new Category { Id = 2, Name = "SciFi", DisplayOrder = "2" },
                 new Category { Id = 3, Name = "History", DisplayOrder = "3" }
                );

            modelBuilder.Entity<Product>().HasData(
                 new Product {
                    Id = 1,
                    Title = "A Heartbreaking Work of Staggering Genius: Pulitzer Prize Finalist ",
                    Description = "A Heartbreaking Work of Staggering Genius is the moving memoir of a college senior who, in the space of five weeks, loses both of his parents to cancer and inherits his eight-year-old brother. This exhilarating debut that manages to be simultaneously hilarious and wildly inventive as well as a deeply heartfelt story of the love that holds a family together.",
                    Author = " Dave Eggers",
                    ISBN = "0375725784",
                    ListPrice = 99.0,
                    Price = 90.0,
                    Price100 = 80.0,
                    Price50 = 85.0,
                    CategoryId = 1,
                 },
                 new Product
                 {
                     Id = 2,
                     Title = "Long Way Gone",
                     Description = "This is how wars are fought now: by children, hopped-up on drugs and wielding AK-47s. Children have become soldiers of choice. In the more than fifty conflicts going on worldwide, it is estimated that there are some 300,000 child soldiers. Ishmael Beah used to be one of them.",
                     Author = "Ishmael Beah",
                     ISBN = "9780374531263",
                     ListPrice = 99.0,
                     Price = 90.0,
                     Price50 = 85.0,
                     Price100 = 80.0,
                     CategoryId = 1
                 },
                 new Product
                 {
                     Id = 3,
                     Title = "A Moveable Feast: The Restored Edition",
                     Description = "Published posthumously in 1964, A Moveable Feast remains one of Ernest Hemingway’s most enduring works. Since Hemingway’s personal papers were released in 1979, scholars have examined the changes made to the text before publication. Now, this special restored edition presents the original manuscript as the author prepared it to be published.",
                     Author = " Ernest Hemingway",
                     ISBN = "143918271X",
                     ListPrice = 99.0,
                     Price = 90.0,
                     Price50 = 85.0,
                     Price100 = 80.0,
                     CategoryId = 1

                 },
                 new Product
                 {
                     Id = 4,
                     Title = "American Caesar: Douglas MacArthur 1880 - 1964",
                     Description = "Inspiring, outrageous... A thundering paradox of a man. Douglas MacArthur, one of only five men in history to have achieved the rank of General of the United States Army. He served in World Wars I, II, and the Korean War, and is famous for stating that \"in war, there is no substitute for victory.\"\r\n",
                     Author = "William Manchester",
                     ISBN = "0316024740",
                     ListPrice = 99.0,
                     Price = 90.0,
                     Price50 = 85.0,
                     Price100 = 80.0,
                     CategoryId = 2

                 },
                 new Product
                 {
                     Id = 5,
                     Title = "American Prometheus: The Inspiration for the Major Motion Picture OPPENHEIMER ",
                     Description = "THE INSPIRATION FOR THE ACADEMY AWARD®-WINNING MAJOR MOTION PICTURE OPPENHEIMER • \"A riveting account of one of history’s most essential and paradoxical figures.”—Christopher Nolan",
                     Author = "Kai Bird, Martin J. Sherwin",
                     ISBN = "0375726268",
                     ListPrice = 99.0,
                     Price = 90.0,
                     Price50 = 85.0,
                     Price100 = 80.0,
                     CategoryId = 2
                 },
                 new Product
                 {
                     Id = 6,
                     Title = "Just Kids: A National Book Award Winner",
                     Description = "“Reading rocker Smith’s account of her relationship with photographer Robert Mapplethorpe, it’s hard not to believe in fate. How else to explain the chance encounter that threw them together, allowing both to blossom? Quirky and spellbinding.” -- People",
                     Author = "Patti Smith",
                     ISBN = "0060936228",
                     ListPrice = 99.0,
                     Price = 90.0,
                     Price50 = 85.0,
                     Price100 = 80.0,
                     CategoryId = 3
                 }
                );
        }
    }
}
