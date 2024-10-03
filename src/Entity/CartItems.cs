using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.src.Entity
{
    public class CartItems
    {
        public Guid CartItemsId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; } // = Book.Price * cartItems.Quantity

        //connections
        public Guid BookId { get; set; }
        public Book Book { get; set; } //
        public Guid CartId { get; set; } //
       // public Guid? OrderId { get; set; } //how?1:20 Present the relation bridge table
    }
}
