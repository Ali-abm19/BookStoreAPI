using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.src.Utils;
using BookStore.src.Database;
using BookStore.src.Entity;
using Microsoft.EntityFrameworkCore;

namespace BookStore.src.Repository
{
    public class OrderRepository//m talk to datda base by query bu using DI
    {
        //first i want to access order table 
        protected DbSet<Order> _order;
        //access to database its self
        protected DatabaseContext _databaseContext;
        //now by using DI use costructor INJUCT the database into repo class 

        public OrderRepository(DatabaseContext databaseContext)
        {
            //INJUCT the database into repo class 
            _databaseContext = databaseContext;
            //then initalize the order table in the database 
            _order = databaseContext.Set<Order>();

        }

        //method 

        //create order
        public async Task<Order> CreateOneAsync(Order newOrder)
        {
            await _order.AddAsync(newOrder);
            await _databaseContext.SaveChangesAsync();
            return newOrder;
        }

        //get order by id 

        public async Task<Order?> GetByIdAsync(Guid id)
        {

            return await _order.FindAsync(id); ;
        }
        //delete order or cancel 
        public async Task<bool> DeleteOneAsync(Order Order)
        {
            _order.Remove(Order);
            await _databaseContext.SaveChangesAsync();
            return true;
        }
        //update method
        public async Task<bool> UpdateOneAsync(Order UpdateOrder)
        {
            _order.Update(UpdateOrder);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        //get all order 

        // public async Task<List<Order>> GetAllAsync(PaginationOptions paginationOptions)
        // {

        //     var result = _order.Where(o =>
        //            o.DateCreated.ToString("yyyy-MM-dd").Contains(paginationOptions.Search));

        //     return await result.Skip(paginationOptions.Offset).Take(paginationOptions.Limit).ToListAsync();

        //     // return await _order.ToListAsync();
        // }
        public async Task<List<Order>> GetAllAsync(PaginationOptions paginationOptions)
        {
            if (paginationOptions == null)
                throw new ArgumentNullException(nameof(paginationOptions));

            if (paginationOptions.Offset < 0)
                throw new ArgumentOutOfRangeException(nameof(paginationOptions.Offset), "Offset must be non-negative.");

            if (paginationOptions.Limit <= 0)
                throw new ArgumentOutOfRangeException(nameof(paginationOptions.Limit), "Limit must be positive.");

            var result = _order.AsQueryable();

            if (!string.IsNullOrEmpty(paginationOptions.Search))
            {
                if (DateTime.TryParse(paginationOptions.Search, out DateTime searchDate))
                {
                    // Ensure the search date is treated as UTC
                    searchDate = DateTime.SpecifyKind(searchDate, DateTimeKind.Utc);
                    result = result.Where(o => o.DateCreated.Date == searchDate.Date);
                }
                else
                {
                    return new List<Order>(); // Handle invalid date format
                }
            }

            return await result.Skip(paginationOptions.Offset).Take(paginationOptions.Limit).ToListAsync();
        }

    }
}