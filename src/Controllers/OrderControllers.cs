using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BookStore.src.Entity;
using BookStore.src.Services.order;
using BookStore.src.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static BookStore.src.DTO.OrderDTO;

namespace BookStore.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrdersController : ControllerBase
    {
        protected readonly IOrderServices _orderServices;

        public OrdersController(IOrderServices services)
        {
            _orderServices = services;
        }

        //Create Order
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<OrderReadDto>> CreateOnAsync(
            [FromBody] OrderCreateDto orderCreateDto
        )
        {
            //by token
            var authenticateClaims = HttpContext.User;
            // get user id by claims
            var userId = authenticateClaims
                .FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!
                .Value;
            // string => Guid
            var userGuid = new Guid(userId);

            return await _orderServices.CreateOneAsync(userGuid, orderCreateDto);
        }

        //Get all Orders Info
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<OrderReadDto>>> GetAll()
        {
            try
            {
                // Get all orders for admin
                var orders = await _orderServices.GetAllAsync();
                return Ok(orders); // Return the list of orders
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message); // Return specific status and message
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred."); // General error handling
            }
        }

        // Delete Order

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteOrder(Guid id)
        {
            try
            {
                var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userGuid))
                {
                    return Unauthorized("User not authenticated.");
                }

                var isAdmin = HttpContext.User.IsInRole("Admin");

                // Call the service to delete the order
                var deleteSuccessful = await _orderServices.DeleteOneAsync(id, userGuid, isAdmin);

                if (!deleteSuccessful)
                {
                    return NotFound("Order could not be deleted.");
                }

                return NoContent(); // Return 204 No Content on successful deletion
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message); // Return specific status and message
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred: " + ex.Message); // General error handling
            }
        }

        //Get Order by UserId
        [HttpGet("userId/{id}")]
        /*Manar, please.. why would we get the orders by userId? this is OrderController,
        the expetations for this endpoint would be to get the Order by OrderId
        You already did the UserOrder method which uses claims to return the user's orders, this is not needed. @ali */
        public async Task<ActionResult<OrderReadDto>> GetById([FromRoute] Guid id)
        {
            var order = await _orderServices.GetByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpGet("{orderId}")]
        [Authorize(Roles = "Admin")]
        //This method takes the OrderId and returns the Order if found. easy, simple, and nice. @ali
        public async Task<ActionResult<OrderReadDto>> GetByOrderId([FromRoute] Guid orderId)
        {
            var order = await _orderServices.FindOrderByIdAsync(orderId);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        //Get User Orders
        [HttpGet("UserOrders")] //Route for getting user orders
        [Authorize]
        public async Task<ActionResult<List<OrderReadDto>>> GetAllUserOrder()
        {
            // Get user ID from claims
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            // Check if user is authenticated
            if (userIdClaim == null)
            {
                return Unauthorized("User not authenticated.");
            }

            if (!Guid.TryParse(userIdClaim.Value, out var userGuid))
            {
                return BadRequest("Invalid user ID.");
            }

            // Get orders belonging to the user
            var orders = await _orderServices.GetAllByUserIdAsync(userGuid);

            // Check if any orders were found
            if (orders == null || !orders.Any())
            {
                return NotFound("No orders found for the user.");
            }

            // Return the list of orders
            return Ok(orders);
        }

        //Update By Order Id
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")] //only admin can edit the order. customer can only send requests to support
        public async Task<ActionResult> UpdateOrder(Guid id, [FromBody] OrderUpdateDto orderUpdate)
        {
            try
            {
                // Retrieve the existing order to get the current status
                var existingOrder = await _orderServices.FindOrderByIdAsync(id);
                if (existingOrder == null)
                {
                    return NotFound(); // Return 404 if the order doesn't exist
                }

                // Call the service to update the order
                var updateSuccessful = await _orderServices.UpdateOneAsync(id, orderUpdate);

                // If the update was unsuccessful, return NotFound
                if (!updateSuccessful)
                {
                    return BadRequest();
                    /* we already established that the order exist so it makes no sense to return NotFound() @ali*/
                }

                // Retrieve the updated order to get the new status
                var updatedOrder = await _orderServices.FindOrderByIdAsync(id);

                // Prepare the response with previous and current status
                var response = new
                {
                    PreviousStatus = existingOrder.OrderStatus.ToString(), // Convert enum to string
                    CurrentStatus = updatedOrder.OrderStatus.ToString() // Convert enum to string
                    ,
                };

                return Ok(response); // Return 200 OK with the status information
            }
            catch (CustomException ex)
            {
                // Handle specific custom exceptions
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
