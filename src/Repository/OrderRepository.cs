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

        //Create Order
        public async Task<Order?> CreateOneAsync(Order newOrder)
        {
            await _order.AddAsync(newOrder);
            await _databaseContext.SaveChangesAsync();
            var orderWithCaryItems = await _order
           .Include(o => o.CartItems)
           .ThenInclude(od => od.Book)
           .FirstOrDefaultAsync(o => o.OrderId == newOrder.OrderId);
            return orderWithCaryItems;

        }
        //Get all Orders Info
        public async Task<List<Order>> GetAllAsync()
        {
            return await _order
                .Include(o => o.CartItems)
                .ThenInclude(od => od.Book)
                .ToListAsync();
        }



        //Get Order Or User by id 
        public async Task<List<Order>> GetByIdAsync(Guid id)
        {

            return await _order
            .Include(o => o.CartItems)
            .ThenInclude(od => od.Book)
            .Where(o => o.UserId == id)
            .ToListAsync();

        }

        //Delete Order  
        public async Task<bool> DeleteOneAsync(Order Order)
        {
            _order.Remove(Order);
            await _databaseContext.SaveChangesAsync();
            return true;
        }




        /// <summary>
        /// /
        /// </summary>
        /// <param name="updateOrder"></param>
        /// <returns></returns>

        //update method
        // public async Task<bool> UpdateOneAsync(Order UpdateOrder)
        // {
        //     _order.Update(UpdateOrder);
        //     await _databaseContext.SaveChangesAsync();
        //     return true;
        // }


        public async Task<bool> UpdateOneAsync(Order updateOrder)
        {
            _order.Update(updateOrder);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        // Find Order by ID
        public async Task<Order?> FindOrderByIdAsync(Guid id)
        {
            return await _order
                .Include(o => o.CartItems)
                .ThenInclude(od => od.Book)
                .FirstOrDefaultAsync(o => o.OrderId == id);
            // Ensure this is looking for OrderId
        }




    }
}

//Get Order by UserId
// public async Task<List<Order>> GetAllByUserIdAsync(Guid userId)
// {
//     return await _order
//         .Include(o => o.CartItems)
//         .ThenInclude(od => od.Book)
//         .Where(o => o.UserId == userId)
//         .ToListAsync();
// }

// public async Task<List<Order>> GetAllAsync()
// {
//     return await _order
//         .Include(o => o.CartItems)
//         .ThenInclude(od => od.Book)
//         .ToListAsync(); 
// }