using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.src.Entity;
using BookStore.src.Entity;
using BookStore.src.Services.category;
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

        //ْUpdate Descrption of Category
        [HttpPut("{id}/description")]
        public async Task<IActionResult> UpdateDescription(
            Guid id,
            [FromBody] CategoryUpdateDesDto description
        )
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            { // 404
                return NotFound();
            }
            var UpdateDes = await _categoryService.UpdateDesOneAsync(id, description);
            //204
            return NoContent();
        }

        //ْUpdate Name of Category
        [HttpPut("{id}/name")]
        public async Task<IActionResult> UpdateCategoryName(
            Guid id,
            [FromBody] CategoryUpdateNameDto name
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
//         public static List<Category> categories = new List<Category>
//         {
//             new Category
//             {
//                 CategoryId = 1,
//                 Description =
//                     "A hypnotically fast-paced, masterful reporting of Harry Truman’s first 120 days as president, when he took on Germany, Japan, Stalin, and a secret weapon of unimaginable power—marking the most dramatic rise to greatness in American history.",
//                 categoryname = Category.CategoryName.History,
//             },
//             new Category
//             {
//                 CategoryId = 2,
//                 Description =
//                     "Everything You Need to Know About the World and How It Works encapsulates centuries of scientific thought in one volume. Natural phenomena, revolutionary inventions, scientific facts, and the most up-to-date questions are all explained in detailed text that is complemented by visually arresting graphics.",
//                 categoryname = Category.CategoryName.Science,
//             },
//             new Category
//             {
//                 CategoryId = 3,
//                 Description =
//                     "The Wonderful Things You Will Be has a loving and truthful message that will endure for lifetimes and makes a great gift to the ones you love for any occasion.",
//                 categoryname = Category.CategoryName.Children,
//             },
//             new Category
//             {
//                 CategoryId = 4,
//                 Description =
//                     "The Louvre is the most famous museum in the world. With 8.5 million visitors every year, the Louvre houses and displays many of the most celebrated and important paintings of all time. Every painting on exhibit in the permanent collection, a total of 3,022 works in all, is included in this groundbreaking book.",
//                 categoryname = Category.CategoryName.Art,
//             },
//             new Category
//             {
//                 CategoryId = 5,
//                 Description =
//                     "Forget everything you think you know about your body and food, and discover the new science of how the body heals itself. Learn how to identify the strategies and dosages for using food to transform your resilience and health in Eat to Beat Disease.",
//                 categoryname = Category.CategoryName.Health,
//             },
//         };

//         // GET METHOD

//         [HttpGet]
//         public ActionResult GetCategories()
//         {
//             return Ok(categories);
//         }

//         //GET METHOD BY DYNAMIC ID

//         [HttpGet("{id}")]
//         public ActionResult GetCategoryById(int id)
//         {
//             Category? foundcategory = categories.FirstOrDefault(f => f.CategoryId == id);
//             if (foundcategory == null)
//             {
//                 return NotFound(); // 404
//             }
//             return Ok(foundcategory);
//         }

//         // POST METHOD

//         [HttpPost]
//         public ActionResult CreateCategory(Category newCategory)
//         {
//             categories.Add(newCategory);
//             return CreatedAtAction(
//                 nameof(GetCategoryById),
//                 new { id = newCategory.CategoryId },
//                 newCategory
//             );
//             //201
//         }

//         // PUT METHOD
//         [HttpPut("{id}")]
//         public ActionResult UpdateCategory(int id, Category updateCategory)
//         {
//             Category? foundCategory = categories.FirstOrDefault(u => u.CategoryId == id);
//             if (foundCategory == null)
//             {
//                 return NotFound();
//             }

//             foundCategory.categoryname = updateCategory.categoryname;
//             foundCategory.Description = updateCategory.Description;

//             return Ok(foundCategory);
//         }

//         // DELETE METHOD
//         [HttpDelete("{id}")]
//         public ActionResult DeleteCategory(int id)
//         {
//             Category? foundcategory = categories.FirstOrDefault(f => f.CategoryId == id);
//             if (foundcategory == null)
//             {
//                 return NotFound(); // 404
//             }
//             categories.Remove(foundcategory);
//             return NoContent(); //204
