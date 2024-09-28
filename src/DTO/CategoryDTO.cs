using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BookStore.src.Entity.Category;

namespace BookStore.src.DTO
{
  
         public class CategoryDTO
    {
        //Create Category
        public class CategoryCreateDto
        {
             public CategoryName categoryname { get; set; }
            public string Description { get; set; }
        }

        //Get Category

        public class CategoryReadDto
        {
            public Guid CategoryId { get; set; }
            public string Description { get; set; }
             public CategoryName categoryname { get; set; }
        }

        //Update Category Name
        public class CategoryUpdateNameDto
        {
             public CategoryName categoryname { get; set; }
        }

        //Update Description of the Category
        public class CategoryUpdateDesDto
        {
            public string Description { get; set; }
        }
    }
}
