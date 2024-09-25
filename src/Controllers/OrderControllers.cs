using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BookStore.src.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookStore.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrdersController : ControllerBase
    {
        public static List<Order> orders = new List<Order>
{
    new Order { OrderId = 1, TotalPrice = 999.99f, DateCreated = DateTime.Now, OrderStatus = Order.Status.Completed },
    new Order { OrderId = 2, TotalPrice = 499.99f, DateCreated = DateTime.Now, OrderStatus = Order.Status.Pending },
    new Order { OrderId = 3, TotalPrice = 299.99f, DateCreated = DateTime.Now, OrderStatus = Order.Status.Shipped },
    new Order { OrderId = 4, TotalPrice = 199.99f, DateCreated = DateTime.Now, OrderStatus = Order.Status.Cancelled }
};
        [HttpGet]
        public ActionResult GetOrders()
        {
            return Ok(orders);
        }
        [HttpGet("{id}")]
        public ActionResult GetOrdersById(int id)//
        {
            Order? foundOrder = orders.FirstOrDefault(p => p.OrderId == id);
            if (foundOrder == null)
            {
                return NotFound();
            }
            return Ok(foundOrder);
        }
        [HttpPost]
        public ActionResult CreateOrder(Order newOrder){
            orders.Add(newOrder);
            return CreatedAtAction(nameof(GetOrdersById), new { id = newOrder.OrderId }, newOrder);

        }
        [HttpDelete("{id}")]
        public ActionResult DeleteOrder(int id)
        {
            Order? foundOrder = orders.FirstOrDefault(p => p.OrderId == id);
            if (foundOrder == null)
            {
                return NotFound();
            }

            orders.Remove(foundOrder);

            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateOrder(int id, Order newOrder)
        {
            Order? foundOrder = orders.FirstOrDefault(p => p.OrderId == id);
            if (foundOrder == null)
            {
                return NotFound();
            }
            foundOrder.OrderStatus = newOrder.OrderStatus;
            return Ok(foundOrder);
        }
    }
}