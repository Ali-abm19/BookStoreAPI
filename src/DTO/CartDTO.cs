using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.src.Entity;

namespace BookStore.src.DTO
{
    public class CartDTO
    {
        public class CartCreateDto
        {
            public Guid UserId { get; set; }
           // public List<Guid> CartItemsId { get; set; } //<- we use the Guid instead of the whole object. It will be handled in CartRepo
            // public List<CartItems> CartItems { get; set; } //<- dropped becauese it is very annoying to write the whole obejct list with every request
            //public Guid CartId { get; set; } //generated at runtime
            //public double TotalPrice { get; set; } //calculated at runtime in cartRepo
        }

        /*public class AddCartItemDto
        {
            public CartItems CartItems { get; set; }
        }*/

        // Read cart (get data)
        public class CartReadDto
        {
            public Guid CartId { get; set; }
            public Guid UserId { get; set; }
            public List<CartItems> CartItems { get; set; }
            public double TotalPrice { get; set; }
        }

        // Update cart
        public class CartUpdateDto
        {
            public List<CartItems> CartItems { get; set; }
            //updated in the CartRepo ->   //public double TotalPrice { get; set; } //= totalAmount += CartItems.GetAll.getPrice
        }
    }
}
