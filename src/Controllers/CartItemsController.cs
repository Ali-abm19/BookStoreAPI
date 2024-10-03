using BookStore.src.Services.cartItems;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BookStore.src.DTO.CartItemsDTO;

namespace BookStore.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    // api/v1/carts
    public class CartItemsController : ControllerBase
    {
        protected readonly ICartItemsService _cartItemsService;

        // Dependency Injection
        public CartItemsController(ICartItemsService cartItemsService)
        {
            _cartItemsService = cartItemsService;
        }

        // Create a new cart
        [HttpPost]
        public async Task<ActionResult<CartItemsReadDto>> CreateOne(
            [FromBody] CartItemsCreateDto createDto
        )
        {
            var cartCreated = await _cartItemsService.CreateOneAsync(createDto);
            return Ok(cartCreated);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<CartItemsReadDto>>> GetAll()
        {
            var cartList = await _cartItemsService.GetAllAsync();
            return Ok(cartList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CartItemsReadDto>> GetById([FromRoute] Guid id)
        {
            var foundCart = await _cartItemsService.GetByIdAsync(id);
            if (foundCart == null)
            {
                return NotFound();
            }
            return Ok(foundCart);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOne(Guid id)
        {
            bool isDeleted = await _cartItemsService.DeleteOneAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOne(Guid id, CartItemsUpdateDto updateDto)
        {
            bool isUpdated = await _cartItemsService.UpdateOneAsync(id, updateDto);
            if (!isUpdated)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
