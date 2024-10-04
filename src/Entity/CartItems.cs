using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.src.Entity
{
    public class CartItems
    {
        [Key]
        public Guid CartItemsId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; } // = Book.Price * cartItems.Quantity

        //connections
        [ForeignKey("BookBookId")]
        public Guid BookId { get; set; }
        public Book Book { get; set; } //

        [ForeignKey("CartCartId")]
        public Guid CartId { get; set; } //
                                         // public Guid? OrderId { get; set; } //how?1:20 Present the relation bridge table
    }
}
