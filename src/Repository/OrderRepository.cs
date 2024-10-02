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
    public class OrderRepository
    {
        protected readonly DbSet<Order> _order;
        protected readonly DatabaseContext _databaseContext;

        public OrderRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _order = databaseContext.Set<Order>();

        }

        //method 

        //create order
        public async Task<Order? > CreateOneAsync(Order newOrder)
        {
            await _order.AddAsync(newOrder);
            await _databaseContext.SaveChangesAsync();
            var orderWithCaryItems = await _order
           .Include(o => o.CartItems)
           .ThenInclude(od => od.Book)
           .FirstOrDefaultAsync(o => o.OrderId == newOrder.OrderId);
            return orderWithCaryItems;
    
        }

        //get order by id 

        public async Task<List<Order>> GetByIdAsync(Guid userId)
        {
        
            return await _order
            .Include(o => o.CartItems)
            .ThenInclude(od => od.Book)
            .Where(o => o.UserId == userId)
            .ToListAsync();

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