using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.src.Utils
{
    public class PaginationOptions
    {
        public int Limit { get; set; } = 2;
        public int Offset { get; set; } = 0;

        // search functinality for order 
        //public string Search { get; set; } = string.Empty;
        
        // search functionality for book 
        public string? SearchByAuthor { get; set; } //  author search
        public string? SearchByTitle { get; set; }  //  title search
        public string SortByPrice { get; set; } = ""; // sort by price "high_low" and "low_high"
        public double? MinPrice { get; set; } = 0;
        public double? MaxPrice { get; set; } = 1000;
    }
}