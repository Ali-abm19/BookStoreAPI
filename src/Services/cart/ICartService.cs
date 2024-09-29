using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static BookStore.src.DTO.CartDTO;

namespace BookStore.src.Services.cart
{
    public interface ICartService
    {
        Task<CartReadDto> CreateOneAsync(CartCreateDto createDto);
        Task<List<CartReadDto>> GetAllAsync();
        Task<CartReadDto> GetByIdAsync(Guid id);
        Task<bool> DeleteOneAsync(Guid id);
        Task<bool> UpdateOneAsync(Guid id, CartUpdateDto updateDto);
    }
}
