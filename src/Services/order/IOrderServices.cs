using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.src.Utils;
using static BookStore.src.DTO.OrderDTO;

namespace BookStore.src.Services.order
{
    public interface IOrderServices
    {
        //just the method
        //creat > parameter use like the info we nees the type OrderCreateDto has DateTime that we need to create order 
        Task<OrderReadDto> CreateOneAsync(Guid userGuid, OrderCreateDto orderCreate);

        //get all info for evry order
        Task<List<OrderReadDto>> GetAllAsync(PaginationOptions paginationOptions);
        //get by id 
        Task<List<OrderReadDto>> GetByIdAsync(Guid userId);
        //delete 
        Task<bool> DeleteOneAsync(Guid id);
        //update 
        //Task<bool> UpdateOneAsync(Guid id, OrderUpdateDto orderUpdate);

    }
}