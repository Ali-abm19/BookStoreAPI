using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BookStore.src.DTO.CategoryDTO;

namespace BookStore.src.Services.category
{
    public interface ICategoryService
    {
        //crete method category
        Task<CategoryReadDto> CreateOneAsync(CategoryCreateDto createDto);

        // Get all catgeories
        Task<List<CategoryReadDto>> GetAllAsync();

        //Get Category by id
        Task<CategoryReadDto> GetByIdAsync(Guid CategoryId);

        // Delete Category by id
        Task<bool> DeleteOneAsync(Guid CategoryId);

        // Update Category by id & name
        Task<bool> UpdateOneAsync(Guid CategoryId, CategoryUpdateNameDto updateDto);

        // Update Category by id & Des
        Task<bool> UpdateDesOneAsync(Guid CategoryId, CategoryUpdateDesDto updateDto);
    }
}
