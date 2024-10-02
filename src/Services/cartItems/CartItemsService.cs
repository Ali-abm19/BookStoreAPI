using AutoMapper;
using BookStore.Repository;
using BookStore.src.Entity;
using BookStore.src.Repository;
using static BookStore.src.DTO.CartItemsDTO;

namespace BookStore.src.Services.cartItems
{
    public class CartItemsService : ICartItemsService
    {
        protected readonly CartItemsRepository _cartItemsRepo;
        protected readonly BookRepository _bookRepository;
        protected readonly IMapper _mapper;

        // Dependency Injection
        public CartItemsService(CartItemsRepository cartItemsRepo, BookRepository bookRepository, IMapper mapper)
        {
            _cartItemsRepo = cartItemsRepo;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        // Create a new cart
        public async Task<CartItemsReadDto> CreateOneAsync(CartItemsCreateDto createDto)
        {
            

            var cartItem = _mapper.Map<CartItemsCreateDto, CartItems>(createDto);
            cartItem.CartItemsId = Guid.NewGuid(); 

            var book = await _bookRepository.GetBookByIdAsync(cartItem.BookId); 
            if (book != null)
            {
                cartItem.Price = book.Price * cartItem.Quantity; 
            }
            else
            {
                throw new Exception("Book not found"); 
            }

            var createdCartItem = await _cartItemsRepo.CreateOneAsync(cartItem);

            return _mapper.Map<CartItems, CartItemsReadDto>(createdCartItem);
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
