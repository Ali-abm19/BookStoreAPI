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
                .Include(o => o.Cart)
                .ThenInclude(c => c.CartItems)
                .ThenInclude(od => od.Book)
                .FirstOrDefaultAsync(o => o.OrderId == newOrder.OrderId);
            return orderWithCaryItems;
        }

        //Get all Orders Info
        public async Task<List<Order>> GetAllAsync()
        {
            return await _order
                .Include(o => o.Cart)
                .ThenInclude(c => c.CartItems)
                .ThenInclude(od => od.Book)
                .ToListAsync();
        }

        //Get Order Or User by id
        //is this: Order "For" user? @ali
        public async Task<List<Order>> GetByIdAsync(Guid id) 
        // apparently this returns the orders given a UserId.
        // I disagree with it wholeheartedly but we don't have
        // time to discuss it nor change it. @ali
        {
            return await _order
                .Include(o => o.Cart)
                .ThenInclude(c => c.CartItems)
                .ThenInclude(od => od.Book)
                .Where(o => o.UserId == id)
                .ToListAsync();
        }

        //Delete Order
        public async Task<bool> DeleteOneAsync(Order order)
        {
            _order.Remove(order);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

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
                .Include(o => o.Cart)
                .ThenInclude(c => c.CartItems)
                .ThenInclude(od => od.Book)
                .FirstOrDefaultAsync(o => o.OrderId == id);
            // Ensure this is looking for OrderId
        }
    }
}
