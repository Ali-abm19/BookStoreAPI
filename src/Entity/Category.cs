using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.src.Entity
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public CategoryName categoryname { get; set; }

        public enum CategoryName
        {
            History,
            Science,
            Children,
            Art,
            Health,
        }
    }
}
