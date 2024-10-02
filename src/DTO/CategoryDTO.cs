using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.src.Entity;
using static BookStore.src.Entity.Category;

namespace BookStore.src.DTO
{
    public class CategoryDTO
    {
        //Create Category
        public class CategoryCreateDto
        {
            public string CategoryName { get; set; }
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
            public string CategoryName { get; set; }
            public string Description { get; set; }
        }


    }
}
