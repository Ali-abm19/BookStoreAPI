using BookStore.src.Services.cart;
using Microsoft.AspNetCore.Mvc;
using static BookStore.src.DTO.BookDTO;
using static BookStore.src.DTO.CartDTO;
using static BookStore.src.DTO.CartItemsDTO;

namespace BookStore.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    // api/v1/carts
    public class CartsController : ControllerBase
    {
        protected readonly ICartService _cartService;

        // Dependency Injection
        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // Create a new cart
        [HttpPost]
        public async Task<ActionResult<CartReadDto>> CreateOne([FromBody] CartCreateDto createDto)
        {
            var cartCreated = await _cartService.CreateOneAsync(createDto);
            return Ok(cartCreated);
        }

        [HttpGet]
        public async Task<ActionResult<List<CartReadDto>>> GetAll()
        {
            var cartList = await _cartService.GetAllAsync();
            return Ok(cartList);
        }

        // Get cart by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<CartReadDto>> GetById([FromRoute] Guid id)
        {
            var foundCart = await _cartService.GetByIdAsync(id);
            if (foundCart == null)
            {
                return NotFound();
            }

            // Calculate TotalPrice if it hasn't been calculated already
            foundCart.TotalPrice =
                foundCart.CartItems?.Sum(item => item.Price * item.Quantity) ?? 0;

            return Ok(foundCart);
        }

        // Delete cart by ID
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOne(Guid id)
        {
            bool isDeleted = await _cartService.DeleteOneAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        // Update cart by ID
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOne(Guid id, CartUpdateDto updateDto)
        {
            bool isUpdated = await _cartService.UpdateOneAsync(id, updateDto);
            if (!isUpdated)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
