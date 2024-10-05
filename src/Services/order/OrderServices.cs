using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.src.Entity;
using BookStore.src.Repository;
using BookStore.src.Utils;
using static BookStore.src.DTO.OrderDTO;

namespace BookStore.src.Services.order
{
    public class OrderServices : IOrderServices
    {
        protected readonly OrderRepository _orderRepository;
        protected readonly IMapper _mapper;
        protected readonly CartRepository _cartRepo;

        public OrderServices(
            OrderRepository orderRepository,
            IMapper mapper,
            CartRepository cartRepo
        )
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _cartRepo = cartRepo;
        }

        //Create Order
        public async Task<OrderReadDto> CreateOneAsync(Guid userId, OrderCreateDto orderCreate)
        {
            var carts = await _cartRepo.GetAllAsync();
            var userCart = carts.FirstOrDefault(c => c.UserId == userId);

            var order = _mapper.Map<OrderCreateDto, Order>(orderCreate); //orderCreate only has CartId
            order.UserId = userId;
            if (userCart != null)
            {
                order.CartId = userCart.CartId;
                order.Cart = userCart;
                order.DateCreated = DateTime.UtcNow;
                order.TotalPrice = userCart.TotalPrice;
                order.OrderStatus = Order.Status.Pending;
                await _orderRepository.CreateOneAsync(order);
            }
            return _mapper.Map<Order, OrderReadDto>(order);
        }

        //Get Order by id

        //Get all Orders Info
        public async Task<List<OrderReadDto>> GetAllAsync()
        {
            var orderList = await _orderRepository.GetAllAsync();

            if (orderList == null || !orderList.Any())
            {
                throw CustomException.NotFound("No orders found.");
            }

            return _mapper.Map<List<Order>, List<OrderReadDto>>(orderList);
        }

        //Get by UserId
        public async Task<List<OrderReadDto>> GetByIdAsync(Guid id)
        {
            var orders = await _orderRepository.GetByIdAsync(id);
            var orderList = _mapper.Map<List<Order>, List<OrderReadDto>>(orders);
            return orderList;
        }

        public async Task<List<OrderReadDto>> GetAllByUserIdAsync(Guid userId)
        {
            // Check if userId is empty
            if (userId == Guid.Empty)
            {
                throw CustomException.BadRequest("User ID cannot be empty.");
            }

            // Fetch orders for the given user ID
            var orders = await _orderRepository.GetByIdAsync(userId);

            // Check if orders are found
            if (orders == null || orders.Count == 0)
            {
                throw CustomException.NotFound("No orders found for the user.");
            }

            return _mapper.Map<List<Order>, List<OrderReadDto>>(orders);
        }

        // //Delete Order

        public async Task<bool> DeleteOneAsync(Guid id, Guid userId, bool isAdmin)
        {
            var foundOrderById = await _orderRepository.FindOrderByIdAsync(id);

            if (foundOrderById == null)
            {
                throw CustomException.NotFound($"Order with ID {id} cannot be found!");
            }

            // If the user is not an admin, check if they are the owner of the order
            if (!isAdmin && foundOrderById.UserId != userId)
            {
                throw CustomException.Forbidden("You do not have permission to delete this order.");
            }

            // Proceed to delete the order
            return await _orderRepository.DeleteOneAsync(foundOrderById);
        }

        public async Task<bool> UpdateOneAsync(Guid id, OrderUpdateDto orderUpdate)
        {
            // Retrieve the order by ID using the new method
            var foundOrderById = await _orderRepository.FindOrderByIdAsync(id);

            // Check if the order exists
            if (foundOrderById == null)
            {
                throw CustomException.NotFound("Order not found with the specified ID.");
            }

            // Update the properties selectively
            //if (orderUpdate.DateUpdated.HasValue){foundOrderById.DateUpdated = orderUpdate.DateUpdated.Value;}
            /* DateUpdated should update to Date.Now because this is an update method,
            so by invoking it we are updating the date.
            Request sender shouldn't input a date into the request to begin with. @ali*/
            foundOrderById.DateUpdated = DateTime.UtcNow;
            if (orderUpdate.TotalPrice.HasValue)
            {
                foundOrderById.TotalPrice = orderUpdate.TotalPrice.Value;
                /*I don't think the price should be changed
                but maybe the customer was offered a discount or something so i'll keep this for now @ali*/
            }

            foundOrderById.OrderStatus = orderUpdate.OrderStatus; // Update order status

            // Call the repository to update the order
            return await _orderRepository.UpdateOneAsync(foundOrderById);
        }

        public async Task<OrderReadDto> FindOrderByIdAsync(Guid id)
        {
            // Use the repository to find the order by ID
            var order = await _orderRepository.FindOrderByIdAsync(id);

            // Check if the order exists
            if (order == null)
            {
                throw CustomException.NotFound("Order not found with the specified ID.");
            }

            // Map the found order to OrderReadDto and return it
            return _mapper.Map<Order, OrderReadDto>(order);
        }
    }
}
