using BookStore.Repository;
using BookStore.src.Database;
using BookStore.src.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace BookStore.src.Repository
{
    public class CartRepository
    {
       
        protected DbSet<Cart> _cart;
        protected DatabaseContext _databaseContext;

        public CartRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _cart = databaseContext.Set<Cart>();
        }

        // create new cart

        public async Task<Cart> CreateOneAsync(Cart newCart)
        {
            //ali

            if (newCart != null)
            {

                await _cart.AddAsync(newCart);
                await _databaseContext.SaveChangesAsync();
            }
            return newCart;
        }

        public async Task<Cart?> GetByIdAsync(Guid id)
        {
            var cartById = await _cart
                .Include(c => c.CartItems).ThenInclude(c => c.Book)
                .FirstOrDefaultAsync(c => c.CartId == id);
            if (cartById != null && cartById.CartItems != null)
            {
                cartById.TotalPrice = cartById.CartItems.Sum(p => p.Price);
            }
            await _databaseContext.SaveChangesAsync();

            return cartById;
        }

        public async Task<List<Cart>> GetAllAsync()
        {
            var cartWithUpdatedPrices = await _cart.Include(c => c.CartItems).ThenInclude(b => b.Book).ToListAsync();
            cartWithUpdatedPrices.ForEach(c => c.TotalPrice = c.CartItems.Sum(c => c.Price));

            await _databaseContext.SaveChangesAsync();
            return cartWithUpdatedPrices;
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
