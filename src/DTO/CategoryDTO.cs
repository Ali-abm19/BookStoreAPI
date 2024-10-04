using System.ComponentModel.DataAnnotations;
using BookStore.src.Entity;

namespace BookStore.src.DTO
{
    public class CategoryDTO
    {
        //Create Category
        public class CategoryCreateDto
        {
            [Required(ErrorMessage = "Category name is required")]
            public string CategoryName { get; set; }

            [StringLength(
                120,
                MinimumLength = 10,
                ErrorMessage = "Description must be between 10 and 120 characters."
            )]
            public string Description { get; set; }
        }

        //Get Category

        public class CategoryReadDto
        {
            public Guid CategoryId { get; set; }
            public string Description { get; set; }
            public string CategoryName { get; set; }
            public List<Book> Books { get; set; }
        }

        //Update Category Name + Des
        public class CategoryUpdateDto
        {
            [Required(ErrorMessage = "Category name is required")]
            public string? CategoryName { get; set; }

            [StringLength(
                120,
                MinimumLength = 10,
                ErrorMessage = "Description must be between 10 and 120 characters."
            )]
            public string? Description { get; set; }
        }
    }
}
