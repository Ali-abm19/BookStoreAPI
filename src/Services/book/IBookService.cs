using BookStore.src.Entity;
using static BookStore.src.DTO.BookDTO;
using  BookStore.src.Utils;
namespace BookStore.src.Services.book
{
    public interface IBookService
    {
        Task<ReadBookDto> CreateOneAsync(CreateBookDto createDto);
        Task<List<ReadBookDto>> GetAllAsync(PaginationOptions paginationOptions);
        Task<ReadBookDto> GetBookByIdAsync(Guid id);
        Task<bool> DeleteOneAsync(Guid id);
        Task<bool> UpdateOneAsync(Guid id, UpdateBookDto updateDto);
        Task<List<ReadBookDto>> GetAllAsyncWithConditions();
    }
}
