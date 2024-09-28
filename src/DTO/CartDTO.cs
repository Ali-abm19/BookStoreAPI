using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.src.DTO
{
    public class CartDTO
    {
        public class CartCreateDto
        {
            public Guid UserId { get; set; }
            public int Quantity { get; set; }
            public double Price { get; set; }
        }

        // Read cart (get data)
        public class CartReadDto
        {
            public Guid CartId { get; set; }
            public Guid UserId { get; set; }
            public int Quantity { get; set; }
            public double Price { get; set; }
        }

        // Update cart
        public class CartUpdateDto
        {
            public int Quantity { get; set; }
            public double Price { get; set; }
        }
    }
    }
