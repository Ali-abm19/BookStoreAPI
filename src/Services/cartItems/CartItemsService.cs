using AutoMapper;
using BookStore.Repository;
using BookStore.src.Entity;
using BookStore.src.Repository;
using BookStore.src.Utils;
using static BookStore.src.DTO.CartItemsDTO;

namespace BookStore.src.Services.cartItems
{
    public class CartItemsService : ICartItemsService
    {
        protected readonly CartItemsRepository _cartItemsRepo;
        protected readonly BookRepository _bookRepository;
        protected readonly IMapper _mapper;

        public CartItemsService(
            CartItemsRepository cartItemsRepo,
            BookRepository bookRepository,
            IMapper mapper
        )
        {
            _cartItemsRepo = cartItemsRepo;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<CartItemsReadDto> CreateOneAsync(CartItemsCreateDto createDto)
        {
            var cartItem = _mapper.Map<CartItemsCreateDto, CartItems>(createDto);
            var book = await _bookRepository.GetBookByIdAsync(cartItem.BookId);

            if (book == null)
            {
                throw CustomException.NotFound("Book not found");
            }

            // Check if there is enough quantity of the book available
            if (book.StockQuantity < cartItem.Quantity)
            {
                throw CustomException.BadRequest(
                    $"Not enough stock available for book: {book.Title}. Available: {book.StockQuantity}"
                );
            }

            cartItem.Price = book.Price * cartItem.Quantity; // Update the price of item based on the book price

            book.StockQuantity -= cartItem.Quantity; // Decrease the book quantity by the quantity in the cart item

            await _bookRepository.UpdateOneAsync(book); // // Update the book quantity in the repository

            var createdCartItem = await _cartItemsRepo.CreateOneAsync(cartItem); // Create the cart item
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
            var foundCartItem = await _cartItemsRepo.GetByIdAsync(id);
            // Handle error if not found
            if (foundCartItem == null)
            {
                throw CustomException.NotFound("CartItems not found");
            }
            return _mapper.Map<CartItems, CartItemsReadDto>(foundCartItem);
        }

        // Delete a cart by ID
        public async Task<bool> DeleteOneAsync(Guid id)
        {
            var foundCartItem = await _cartItemsRepo.GetByIdAsync(id);
            if (foundCartItem == null)
            {
                throw CustomException.NotFound($"CartItem with {id} cannot be found for deletion!");
            }

            bool isDeleted = await _cartItemsRepo.DeleteOneAsync(foundCartItem);
            return isDeleted;
        }

        public async Task<bool> UpdateOneAsync(Guid id, CartItemsUpdateDto updateDto)
        {
            var foundCartItem = await _cartItemsRepo.GetByIdAsync(id);
            var book = await _bookRepository.GetBookByIdAsync(foundCartItem.BookId);

            //                     5 old > new 4
            if (foundCartItem.Quantity > updateDto.Quantity)
            {
                book.StockQuantity++;
            }
            else if (foundCartItem.Quantity < updateDto.Quantity)
            {
                book.StockQuantity--;
            }

            await _bookRepository.UpdateOneAsync(book);

            if (foundCartItem == null)
            {
                throw CustomException.NotFound($"CartItem with {id} cannot be found for updating!");
            }

            _mapper.Map(updateDto, foundCartItem);
            return await _cartItemsRepo.UpdateOneAsync(foundCartItem);
        }
    }
}
