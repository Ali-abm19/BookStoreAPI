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
        public OrderServices(OrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;

        }
        //Create Order
        public async Task<OrderReadDto> CreateOneAsync(Guid userId, OrderCreateDto orderCreate)
        {
            var order = _mapper.Map<OrderCreateDto, Order>(orderCreate);
            order.UserId = userId;
            await _orderRepository.CreateOneAsync(order);
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
            if (orders == null || !orders.Any())
            {
                throw CustomException.NotFound("No orders found for the user.");
            }

            return _mapper.Map<List<Order>, List<OrderReadDto>>(orders);
        }





        //Delete Order 
        public async Task<bool> DeleteOneAsync(Guid id)
        {

            var foundOrdersById = await _orderRepository.GetByIdAsync(id);

            var foundOrderById = foundOrdersById.FirstOrDefault();

            if (foundOrderById == null)
            {
                throw CustomException.NotFound($"Order with ID {id} cannot be found!");

            }

            bool deletedOrderById = await _orderRepository.DeleteOneAsync(foundOrderById);

            return deletedOrderById;
        }
/// <summary>
/// ////////
/// </summary>
/// <param name="id"></param>
/// <param name="orderUpdate"></param>
/// <returns></returns>

        //updat Order status
        // public async Task<bool> UpdateOneAsync(Guid id, OrderUpdateDto orderUpdate)
        // {
        //     var foundOrderById = await _orderRepository.GetByIdAsync(id);

        //     if (foundOrderById == null)
        //     {
        //         return false; 
        //     }

        //     _mapper.Map(orderUpdate, foundOrderById);
        //     return await _orderRepository.UpdateOneAsync(foundOrderById);
        // }

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
            if (orderUpdate.DateUpdated.HasValue)
            {
                foundOrderById.DateUpdated = orderUpdate.DateUpdated.Value;
            }

            if (orderUpdate.TotalPrice.HasValue)
            {
                foundOrderById.TotalPrice = orderUpdate.TotalPrice.Value;
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