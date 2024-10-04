using System.ComponentModel.DataAnnotations;

namespace BookStore.src.Entity
{
    public class Category
    {
        public Guid CategoryId { get; set; }

        [StringLength(
            120,
            MinimumLength = 10,
            ErrorMessage = "Description must be between 10 and 120 characters."
        )]
        public string Description { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        public string CategoryName { get; set; }

        //connections to other entities
        //public List<Book> Books { get; set; }
    }
}
