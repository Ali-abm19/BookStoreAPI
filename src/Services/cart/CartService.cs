using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using BookStore.src.Entity;
using BookStore.src.Repository;
using BookStore.src.Utils;
using static BookStore.src.DTO.CartDTO;

namespace BookStore.src.Services.cart
{
    public class CartService : ICartService
    {
        protected readonly CartRepository _cartRepo;
        protected readonly IMapper _mapper;
        protected readonly BookRepository _bookRepo;
        protected readonly OrderRepository _orderRepo;

        public CartService(
            CartRepository cartRepo,
            BookRepository bookRepo,
            IMapper mapper,
            OrderRepository orderRepo
        )
        {
            _cartRepo = cartRepo;
            _bookRepo = bookRepo;
            _orderRepo = orderRepo;
            _mapper = mapper;
        }

        public async Task<CartReadDto> CreateOneAsync(CartCreateDto createDto)
        {
            var allCarts = await _cartRepo.GetAllAsync();
            var existingCart = allCarts.Find(x => x.UserId == createDto.UserId);
            var ordersByUserId = await _orderRepo.GetByIdAsync(createDto.UserId);
            bool doesCartHaveOrder = false;
            if (existingCart != null)
                doesCartHaveOrder = ordersByUserId.Any(x => x.CartId == existingCart.CartId);

            if (existingCart == null || doesCartHaveOrder)
            {
                var cart = _mapper.Map<CartCreateDto, Cart>(createDto);

                var createdCart = await _cartRepo.CreateOneAsync(cart);

                return _mapper.Map<Cart, CartReadDto>(createdCart);
            }
            else
            {
                return _mapper.Map<Cart, CartReadDto>(existingCart);
            }
        }

        public async Task<List<CartReadDto>> GetAllAsync()
        {
            var cartList = await _cartRepo.GetAllAsync();
            return _mapper.Map<List<Cart>, List<CartReadDto>>(cartList);
        }

        public async Task<CartReadDto> GetByIdAsync(Guid id)
        {
            var foundCart = await _cartRepo.GetByIdAsync(id);
            // Handle error if not found
            if (foundCart == null)
            {
                throw CustomException.NotFound($"Cart with {id} cannot be found!");
            }
            return _mapper.Map<Cart, CartReadDto>(foundCart);
        }

        public async Task<bool> DeleteOneAsync(Guid id)
        {
            var foundCart =
                await _cartRepo.GetByIdAsync(id)
                ?? throw CustomException.NotFound($"Cart with {id} cannot be found for deletion!");

            foreach (var cartItem in foundCart.CartItems)
            {
                var b = await _bookRepo.GetBookByIdAsync(cartItem.BookId);
                b.StockQuantity += cartItem.Quantity;
                await _bookRepo.UpdateOneAsync(b);
            }

            bool isDeleted = await _cartRepo.DeleteOneAsync(foundCart);
            return isDeleted;
        }

        public async Task<bool> UpdateOneAsync(Guid id, CartUpdateDto updateDto)
        {
            var foundCart = await _cartRepo.GetByIdAsync(id);

            if (foundCart == null)
            {
                throw CustomException.NotFound($"Cart with {id}cannot be found for updating!");
            }

            _mapper.Map(updateDto, foundCart);
            return await _cartRepo.UpdateOneAsync(id, foundCart.CartItems);
        }
    }
}
