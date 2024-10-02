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
        private readonly ILogger<OrdersController> _logger; // new

        public OrdersController(IOrderServices services, ILogger<OrdersController> logger)
        {
            _orderServices = services;
            _logger = logger;//new

        }
        //create

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<OrderReadDto>> CreateOnAsync([FromBody] OrderCreateDto orderCreateDto)
        

        {
            //by token
            var authenticateClaims = HttpContext.User;
            // get user id by claims
            var userId = authenticateClaims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            // string => Guid
            var userGuid = new Guid(userId);

            return await _orderServices.CreateOneAsync(userGuid,orderCreateDto);

        }

        [HttpGet]
        public async Task<ActionResult<List<OrderReadDto>>> GetAll([FromQuery] PaginationOptions paginationOptions)
        {
            var orders = await _orderServices.GetAllAsync(paginationOptions);
            return Ok(orders); // Return the list of orders
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderReadDto>> GetById([FromRoute] Guid id)
        {
            var order = await _orderServices.GetByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }


        [HttpGet("user/{userId}")]
        [Authorize]

        public async Task<ActionResult<List<OrderReadDto>>> GetOrdersByUserId([FromRoute] Guid userId)
        {
            var orders = await _orderServices.GetByIdAsync(userId);
            return Ok(orders);
        }

        //         [HttpPut("{id}")]
        //         public ActionResult UpdateOrder(int id, Order newOrder)
        //         {
        //             Order? foundOrder = orders.FirstOrDefault(p => p.OrderId == id);
        //             if (foundOrder == null)
        //             {
        //                 return NotFound();
        //             }
        //             foundOrder.OrderStatus = newOrder.OrderStatus;
        //             return Ok(foundOrder);
        //         }
    }
}
