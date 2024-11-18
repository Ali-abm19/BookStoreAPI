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

        public async Task<OrderReadDto> CreateOneAsync(Guid userId, OrderCreateDto orderCreate)
        {
            var carts = await _cartRepo.GetAllAsync();
            var userCart = carts.FirstOrDefault(c => c.UserId == userId);

            var order = _mapper.Map<OrderCreateDto, Order>(orderCreate);
            order.UserId = userId;
            if (userCart != null)
            {
                order.CartId = userCart.CartId;
                order.Cart = userCart;
                order.DateCreated = DateTime.UtcNow;
                order.TotalPrice = userCart.TotalPrice;
                order.OrderStatus = Order.Status.Pending;
                order.Log.Add(
                    $"Order created at {DateTime.UtcNow} with status {order.OrderStatus}"
                );
                if(order.TotalPrice>0){
                await _orderRepository.CreateOneAsync(order);
                }
                else{
                    throw CustomException.BadRequest("You can't make an empty order");
                }
            }
            return _mapper.Map<Order, OrderReadDto>(order);
        }

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
            if (userId == Guid.Empty)
            {
                throw CustomException.BadRequest("User ID cannot be empty.");
            }

            var orders = await _orderRepository.GetByIdAsync(userId);

            if (orders == null || orders.Count == 0)
            {
                throw CustomException.NotFound("No orders found for the user.");
            }

            return _mapper.Map<List<Order>, List<OrderReadDto>>(orders);
        }

        public async Task<bool> DeleteOneAsync(Guid id, Guid userId, bool isAdmin)
        {
            var foundOrderById = await _orderRepository.FindOrderByIdAsync(id);

            if (foundOrderById == null)
            {
                throw CustomException.NotFound($"Order with ID {id} cannot be found");
            }

            // If the user is not an admin, check if they are the owner of the order
            if (!isAdmin && foundOrderById.UserId != userId)
            {
                throw CustomException.Forbidden("You do not have permission to delete this order.");
            }

            await _cartRepo.DeleteOneAsync(foundOrderById.Cart);
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

            foundOrderById.DateUpdated = DateTime.UtcNow;
            foundOrderById.Log.Add(
                $"Order updated from {foundOrderById.OrderStatus} to {orderUpdate.OrderStatus} at {DateTime.UtcNow}"
            );

            foundOrderById.OrderStatus = orderUpdate.OrderStatus;
            return await _orderRepository.UpdateOneAsync(foundOrderById);
        }

        public async Task<OrderReadDto> FindOrderByIdAsync(Guid id)
        {
            var order =
                await _orderRepository.FindOrderByIdAsync(id)
                ?? throw CustomException.NotFound("Order not found with the specified ID.");
            return _mapper.Map<Order, OrderReadDto>(order);
        }
    }
}
