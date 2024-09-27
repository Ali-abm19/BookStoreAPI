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
        //get the data from database
        //mapper
        //by DI

        protected readonly OrderRepository _orderRepository;
        protected readonly IMapper _mapper;
        public OrderServices(OrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;

        }
        public async Task<OrderReadDto> CreateOneAsync(OrderCreateDto orderCreate)
        {
            var order = _mapper.Map<OrderCreateDto, Order>(orderCreate);
            var orderCreated = await _orderRepository.CreateOneAsync(order);
            return _mapper.Map<Order, OrderReadDto>(orderCreated);
        }

        public async Task<List<OrderReadDto>> GetAllAsync(PaginationOptions paginationOptions)
        {
            var orderList = await _orderRepository.GetAllAsync(paginationOptions);
            return _mapper.Map<List<Order>, List<OrderReadDto>>(orderList);
        }
        public async Task<OrderReadDto> GetByIdAsync(Guid id)
        {
            var foundOrderById = await _orderRepository.GetByIdAsync(id);
            return _mapper.Map<Order, OrderReadDto>(foundOrderById);
        }

        public async Task<bool> DeleteOneAsync(Guid id)
        {

            var foundOrderById = await _orderRepository.GetByIdAsync(id);

            bool deletedOrderById = await _orderRepository.DeleteOneAsync(foundOrderById);

            if (deletedOrderById)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateOneAsync(Guid id, OrderUpdateDto orderUpdate)
        {
            var foundOrderById = await _orderRepository.GetByIdAsync(id);
            bool updatedOrder = await _orderRepository.UpdateOneAsync(foundOrderById);

            if (updatedOrder == null)
            {
                return false;
            }


            _mapper.Map(orderUpdate, foundOrderById);
            return await _orderRepository.UpdateOneAsync(foundOrderById);
        }

    }
}