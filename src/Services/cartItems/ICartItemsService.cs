using static BookStore.src.DTO.CartItemsDTO;

namespace BookStore.src.Services.cartItems
{
    public interface ICartItemsService
    {
        Task<CartItemsReadDto> CreateOneAsync(CartItemsCreateDto createDto);
        Task<List<CartItemsReadDto>> GetAllAsync();
        Task<CartItemsReadDto> GetByIdAsync(Guid id);
        Task<bool> DeleteOneAsync(Guid id);
        Task<bool> UpdateOneAsync(Guid id, CartItemsUpdateDto updateDto);
    }
}
