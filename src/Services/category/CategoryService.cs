using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.src.Entity;
using BookStore.src.Repository;
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
            //to do throw handle errto
            return _mapper.Map<Category, CategoryReadDto>(foundCategory);
        }

        //Delete Category
        public async Task<bool> DeleteOneAsync(Guid CategoryId)
        {
            var foundCategory = await _categoryRepo.GetByIdAsync(CategoryId);
            bool isDeleted = await _categoryRepo.DeleteOneAsync(foundCategory);

            if (isDeleted)
            {
                return true;
            }
            return false;
        }

        //Update Category by name
        public async Task<bool> UpdateOneAsync(Guid CategoryId, CategoryUpdateNameDto updateDto)
        {
            var foundCategory = await _categoryRepo.GetByIdAsync(CategoryId);

            if (foundCategory == null)
            {
                return false;
            }
            _mapper.Map(updateDto, foundCategory);
            return await _categoryRepo.UpdateOneAsync(foundCategory);
        }

        //Update Category by des
        public async Task<bool> UpdateDesOneAsync(Guid CategoryId, CategoryUpdateDesDto updateDto)
        {
            var foundCategory = await _categoryRepo.GetByIdAsync(CategoryId);

            if (foundCategory == null)
            {
                return false;
            }
            _mapper.Map(updateDto, foundCategory);
            return await _categoryRepo.UpdateOneAsync(foundCategory);
        }
    }
}
