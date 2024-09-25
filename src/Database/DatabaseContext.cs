using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.src.Entity;
using Microsoft.EntityFrameworkCore;

namespace BookStore.src.Database
{


    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }



    }
}