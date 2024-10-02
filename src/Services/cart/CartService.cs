using AutoMapper;
using BookStore.src.Entity;
using BookStore.src.Repository;
using static BookStore.src.DTO.CartDTO;

namespace BookStore.src.Services.cart
{
    public class CartService : ICartService
    {
        protected readonly CartRepository _cartRepo;
        protected readonly IMapper _mapper;

        // Dependency Injection
        public CartService(CartRepository cartRepo, IMapper mapper)
        {
            _cartRepo = cartRepo;
            _mapper = mapper;
        }

        // Create a new cart
        
        public async Task<CartReadDto> CreateOneAsync(CartCreateDto createDto)
        {
            var cart = _mapper.Map<CartCreateDto, Cart>(createDto);

            var createdCart = await _cartRepo.CreateOneAsync(cart);

            return _mapper.Map<Cart, CartReadDto>(createdCart);
        }

        // Get all carts
        public async Task<List<CartReadDto>> GetAllAsync()
        {
            var cartList = await _cartRepo.GetAllAsync();
            return _mapper.Map<List<Cart>, List<CartReadDto>>(cartList);
        }

        // Get a cart by ID
        public async Task<CartReadDto> GetByIdAsync(Guid id)
        {
            var foundCart = await _cartRepo.GetByIdAsync(id);
            // Handle error if not found
            if (foundCart == null)
            {
                throw new Exception("Cart not found");
            }
            return _mapper.Map<Cart, CartReadDto>(foundCart);
        }

        // Delete a cart by ID
        public async Task<bool> DeleteOneAsync(Guid id)
        {
            var foundCart = await _cartRepo.GetByIdAsync(id);
            if (foundCart == null)
            {
                return false;
            }

            bool isDeleted = await _cartRepo.DeleteOneAsync(foundCart);
            return isDeleted;
        }

        // Update an existing cart
        public async Task<bool> UpdateOneAsync(Guid id, CartUpdateDto updateDto)
        {
            var foundCart = await _cartRepo.GetByIdAsync(id);

            if (foundCart == null)
            {
                return false;
            }

            _mapper.Map(updateDto, foundCart);
            return await _cartRepo.UpdateOneAsync(foundCart);
        }
    }
}
