using AutoMapper;
using BookStore.Repository;
using BookStore.src.Entity;
using static BookStore.src.DTO.CartItemsDTO;

namespace BookStore.src.Services.cartItems
{
    public class CartItemsService : ICartItemsService
    {
        protected readonly CartItemsRepository _cartItemsRepo;
        protected readonly IMapper _mapper;

        // Dependency Injection
        public CartItemsService(CartItemsRepository cartItemsRepo, IMapper mapper)
        {
            _cartItemsRepo = cartItemsRepo;
            _mapper = mapper;
        }

        // Create a new cart
        public async Task<CartItemsReadDto> CreateOneAsync(CartItemsCreateDto createDto)
        {
            var cart = _mapper.Map<CartItemsCreateDto, CartItems>(createDto);

            var createdCart = await _cartItemsRepo.CreateOneAsync(cart);

            return _mapper.Map<CartItems, CartItemsReadDto>(createdCart);
        }

        // Get all carts
        public async Task<List<CartItemsReadDto>> GetAllAsync()
        {
            var cartList = await _cartItemsRepo.GetAllAsync();
            return _mapper.Map<List<CartItems>, List<CartItemsReadDto>>(cartList);
        }

        // Get a cart by ID
        public async Task<CartItemsReadDto> GetByIdAsync(Guid id)
        {
            var foundCart = await _cartItemsRepo.GetByIdAsync(id);
            // Handle error if not found
            if (foundCart == null)
            {
                throw new Exception("CartItems not found");
            }
            return _mapper.Map<CartItems, CartItemsReadDto>(foundCart);
        }

        // Delete a cart by ID
        public async Task<bool> DeleteOneAsync(Guid id)
        {
            var foundCart = await _cartItemsRepo.GetByIdAsync(id);
            if (foundCart == null)
            {
                return false;
            }

            bool isDeleted = await _cartItemsRepo.DeleteOneAsync(foundCart);
            return isDeleted;
        }

        // Update an existing cart
        public async Task<bool> UpdateOneAsync(Guid id, CartItemsUpdateDto updateDto)
        {
            var foundCart = await _cartItemsRepo.GetByIdAsync(id);

            if (foundCart == null)
            {
                return false;
            }

            _mapper.Map(updateDto, foundCart);
            return await _cartItemsRepo.UpdateOneAsync(foundCart);
        }
    }
}
