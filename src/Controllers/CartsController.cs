using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace bookStore
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartsController : ControllerBase
    {
        //In-memory cart 
        List<Cart> carts = new List<Cart>
{
    new Cart
    {
        CartId = 1,
        UserId = 3,
        Quantity = 3,
        Price = 100,
    },
    new Cart
    {
        CartId = 2,
        UserId = 2,
        Quantity = 5,
        Price = 250,
    }
};

       // GET: api/v1/cart
        [HttpGet]
        public ActionResult GetCarts()
        {
            return Ok(carts); // Return all carts
        }

        // GET: api/v1/cart/{id} by ID
        [HttpGet("{id}")]
        public ActionResult GetCartById(int id)
        {
            var cart = carts.FirstOrDefault(c => c.CartId == id);
            if (cart == null)
            {
                return NotFound(); // 404 Not Found if the cart doesn't exist
            }
            return Ok(cart); // Return the cart object
        }

        // POST: api/v1/cart
        [HttpPost]
        public ActionResult CreateCart(Cart newCart)
        {
            // Add a new cart
            carts.Add(newCart);
            return CreatedAtAction(nameof(GetCartById), new { id = newCart.CartId }, newCart); // Return 201 Created
        }

        // DELETE: api/v1/cart/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCart(int id)
        {
            var cart = carts.FirstOrDefault(c => c.CartId == id);
            if (cart == null)
            {
                return NotFound(); // 404 Not Found if the cart doesn't exist
            }
            carts.Remove(cart);
            return NoContent(); // Return 204 No Content on successful deletion
        }


    }

}