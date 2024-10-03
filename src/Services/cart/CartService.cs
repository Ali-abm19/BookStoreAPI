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
            var foundCart = await _cartRepo.GetByIdAsync(id);
              if (foundCart == null)
            {
                throw CustomException.NotFound($"Cart with {id}cannot be found for deletion!");
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
