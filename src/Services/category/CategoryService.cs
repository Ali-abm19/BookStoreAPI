
using AutoMapper;
using BookStore.src.Entity;
using BookStore.src.Repository;
using BookStore.src.Utils;
using static BookStore.src.DTO.CategoryDTO;

namespace BookStore.src.Services.category
{
    public class CategoryService : ICategoryService
    {
        protected readonly CategoryRepository _categoryRepo;
        protected readonly IMapper _mapper;

        public CategoryService(CategoryRepository categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        //Create Category
        public async Task<CategoryReadDto> CreateOneAsync(CategoryCreateDto createDto)
        {
            var category = _mapper.Map<CategoryCreateDto, Category>(createDto);

            var categoryCreated = await _categoryRepo.CreateOneAsync(category);

            return _mapper.Map<Category, CategoryReadDto>(categoryCreated);
        }

        //Get All Categories
        public async Task<List<CategoryReadDto>> GetAllAsync()
        {
            var categoryList = await _categoryRepo.GetAllAsync();
            return _mapper.Map<List<Category>, List<CategoryReadDto>>(categoryList);
        }

        //Get Category by id
        public async Task<CategoryReadDto> GetByIdAsync(Guid CategoryId)
        {
            var foundCategory = await _categoryRepo.GetByIdAsync(CategoryId);
            if (foundCategory == null)
            {
                throw CustomException.NotFound($"category with {CategoryId} cannot be found! ");
            }
            return _mapper.Map<Category, CategoryReadDto>(foundCategory);
        }

        //Delete Category
        public async Task<bool> DeleteOneAsync(Guid CategoryId)
        {
            var foundCategory = await _categoryRepo.GetByIdAsync(CategoryId);
            if (foundCategory == null)
            {
                throw CustomException.NotFound(
                    $"Category with ID {CategoryId} cannot be found for deletion."
                );
            }
            try
            {
                bool isDeleted = await _categoryRepo.DeleteOneAsync(foundCategory);
                return isDeleted;
            }
            catch (Exception ex)
            {
                throw CustomException.InternalError(
                    $"An error occurred while deleting the category with ID {CategoryId}: {ex.Message}"
                );
            }
        }

        //Update Category by name & des
        public async Task<bool> UpdateOneAsync(Guid CategoryId, CategoryUpdateDto updateDto)
        {
            var foundCategory = await _categoryRepo.GetByIdAsync(CategoryId);

            if (foundCategory == null)
            {
                throw CustomException.NotFound(
                    $"Category with ID {CategoryId} cannot be found for updating."
                );
            }
            _mapper.Map(updateDto, foundCategory);
            return await _categoryRepo.UpdateOneAsync(foundCategory);
        }
    }
}
