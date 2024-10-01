using BookStore.src.Database;
using BookStore.src.Entity;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class CartItemsRepository
    {
        protected DbSet<CartItems> _cartItems;
        protected DatabaseContext _databaseContext;

        public CartItemsRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _cartItems = databaseContext.Set<CartItems>();
        }

        // create new cart
        public async Task<CartItems> CreateOneAsync(CartItems newCart)
        {
            //newCart.Price = newCart.Book.Price * newCart.Quantity;
            await _cartItems.AddAsync(newCart);
            await _databaseContext.SaveChangesAsync();
            return newCart;
        }

        //get cart by ID
        public async Task<CartItems?> GetByIdAsync(Guid id)
        {
            return await _cartItems.FindAsync(id);
        }

        // get all
        public async Task<List<CartItems>> GetAllAsync()
        {
            return await _cartItems.ToListAsync();
        }

        //delete
        public async Task<bool> DeleteOneAsync(CartItems cart)
        {
            _cartItems.Remove(cart);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        //update cart
        public async Task<bool> UpdateOneAsync(CartItems updateCart)
        {
            _cartItems.Update(updateCart);
            await _databaseContext.SaveChangesAsync();
            return true;
        }
    }
}
