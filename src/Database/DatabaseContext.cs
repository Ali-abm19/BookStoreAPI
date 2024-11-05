using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.src.Entity;
using Microsoft.EntityFrameworkCore;
using static BookStore.src.Entity.Order;

namespace BookStore.src.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options) { }

        public DbSet<Category> Category { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItems> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<Role>();
            modelBuilder.HasPostgresEnum<Format>();
            modelBuilder.HasPostgresEnum<Status>();
            // modelBuilder
            //     .Entity<Order>()
            //     .Property(o => o.DateCreated)
            //     .HasDefaultValueSql("CURRENT_TIMESTAMP");

            //modelBuilder.Entity<Book>().Property(b => b.BookFormat).HasColumnType("format");
        }
    }
}
