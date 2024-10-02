using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.src.Entity;
using BookStore.src.Services.category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BookStore.src.DTO.CategoryDTO;

namespace BookStore.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    // api/v1/categories
    public class CategoriesController : ControllerBase
    {
        protected readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService service)
        {
            _categoryService = service;
        }

        // Create category
        [HttpPost]
        public async Task<ActionResult<CategoryReadDto>> CreateOne(
            [FromBody] CategoryCreateDto createDto
        )
        {
            var categoryCreated = await _categoryService.CreateOneAsync(createDto);
            //201
            return Created($"api/v1/categories/{categoryCreated.CategoryId}", categoryCreated);
            // return Ok(categoryCreated);
        }

        //Get all Categories
        [HttpGet]
        public async Task<ActionResult<List<CategoryReadDto>>> GetAll()
        {
            var categoryList = await _categoryService.GetAllAsync();
            return Ok(categoryList);
        }

        //Get Category by Id
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryReadDto>> GetById([FromRoute] Guid id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

      
        //ْUpdate Name & Des of Category
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryName(
            Guid id,
            [FromBody] CategoryUpdateDto name
        )
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            { // 404
                return NotFound();
            }
            var UpdateName = await _categoryService.UpdateOneAsync(id, name);
            //204
            return NoContent();
        }

        //Delete Categoy by id
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var category = _categoryService.GetByIdAsync(id).Result;
            if (category == null)
            {
                return NotFound();
            }

            await _categoryService.DeleteOneAsync(category.CategoryId);
            return NoContent();
        }
    }
}
