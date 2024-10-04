using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.src.Entity;
using BookStore.src.Utils;
using static BookStore.src.DTO.OrderDTO;

namespace BookStore.src.Services.order
{
    public interface IOrderServices
    {
        //The method

        //Create Order
        Task<OrderReadDto> CreateOneAsync(Guid userGuid, OrderCreateDto orderCreate);

        //Get all Orders Info
        Task<List<OrderReadDto>> GetAllAsync();

        //Get Order by UserId
        Task<List<OrderReadDto>> GetByIdAsync(Guid userId);

        //Delete Order 
        Task<bool> DeleteOneAsync(Guid id, Guid userId, bool isAdmin);
        //Get by UserId 
        Task<List<OrderReadDto>> GetAllByUserIdAsync(Guid userId);
        //update 
        Task<bool> UpdateOneAsync(Guid id, OrderUpdateDto orderUpdate);
        // Find Order by ID
        Task<OrderReadDto> FindOrderByIdAsync(Guid id);
    }
}