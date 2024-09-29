using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.src.Entity
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string Description { get; set; }
        public CategoryName categoryname { get; set; }

        //connections to other entities
        public List<Book> Books { get; set; }

        public enum CategoryType 
        {
            History,
            Science,
            Children,
            Art,
            Health,
        }
    }
}
