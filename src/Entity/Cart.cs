using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.src.Entity
{

    public class Cart
    {
        [Key]
        public Guid CartId { get; set; }

        //public int Quantity { get; set; }
        public double TotalPrice { get; set; } //= totalAmount += CartItems.GetAll.getPrice

        // connnections with other entities
        //public User User { get; set; }

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
   
        public List<CartItems>? CartItems { get; set; }
    }

}
