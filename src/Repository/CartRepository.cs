using BookStore.Repository;
using BookStore.src.Database;
using BookStore.src.Entity;
using Microsoft.EntityFrameworkCore;

namespace BookStore.src.Repository
{
    public class CartRepository
    {
        // table
        protected DbSet<Cart> _cart;
        protected DatabaseContext _databaseContext;


        public CartRepository(
            DatabaseContext databaseContext
        )
        {
            _databaseContext = databaseContext;
            _cart = databaseContext.Set<Cart>();
        }

        // create new cart

        public async Task<Cart> CreateOneAsync(Cart newCart)
        {
  //ali 
//             if (newCart == null)
//             {
//                 return newCart;
//             }

  //
            // Initialize TotalPrice to 0
            newCart.TotalPrice = 0;

            if (newCart.CartItems != null && newCart.CartItems.Any())
            {
                // Calculate TotalPrice based on quantity and price
                newCart.TotalPrice = newCart.CartItems.Sum(item => item.Price * item.Quantity);
            }

            await _cart.AddAsync(newCart);
            await _databaseContext.SaveChangesAsync();
            return newCart;
        }

        public async Task<Cart?> GetByIdAsync(Guid id)
        {
            return await _cart
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.CartId == id);
        }

  //ali
//         public async Task<List<Cart>> GetAllAsync()
//         {
//             return await _cart.Include(C => C.CartItems).ToListAsync();
  
        public async Task<List<Cart>> GetAllAsync()
        {
            return await _cart
                .Include(c => c.CartItems) // Include CartItems 
                .ToListAsync();
        }

        public async Task<bool> DeleteOneAsync(Cart cart)
        {
            _cart.Remove(cart);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateOneAsync(Guid id, List<CartItems> newCartItems)
        {
            Cart updateCart = await GetByIdAsync(id);
            if (updateCart != null)
            {
                updateCart.CartItems = newCartItems; //update the cartitems //check to make sure that this is necessary. I think the mapper handles it in the Cart service so we might be asigning the same value
                updateCart.TotalPrice = updateCart.CartItems.Sum(p => p.Price); //update the total price
                _cart.Update(updateCart);
                await _databaseContext.SaveChangesAsync();
                return true;
            }
                return false;
            
        }
    }
}
