using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BookStore.src.Entity;
using BookStore.src.Services.order;
using BookStore.src.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static BookStore.src.DTO.OrderDTO;

namespace BookStore.src.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {

        protected readonly IOrderServices _orderServices;
        public OrdersController(IOrderServices services)
        {
            _orderServices = services;

        }
        //create

        [HttpPost]
        public async Task<ActionResult<OrderReadDto>> CreateOne([FromBody] OrderCreateDto orderCreate)
        {
            var orderCreated = await _orderServices.CreateOneAsync(orderCreate);

            return Created($"api/v1/orders/{orderCreated.OrderId}", orderCreated);
            // return Ok(orderCreated);


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
