using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace BookStore.src.Entity
{
    public class Order
    {
        //[Key] //I would add this but I don't want to tempt fate and mess with the DB build at this point
        public Guid OrderId { get; set; }

        //this is calculated in the backend so no need for annotations @ali
        public double TotalPrice { get; set; } //good

        //date created should be generated automatically so ill make it optional @ali
        public DateTime? DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? DateUpdated { get; set; }

        [Required(ErrorMessage = "You must choose a status")]
        public Status OrderStatus { get; set; }

        public List<string> Log { get; set; } =[];

        // connections with other entities
        public Guid UserId { get; set; }
        public Guid CartId { get; set; }
        public Cart Cart { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum Status
        {
            Completed, //0
            Pending, //1
            Shipped, //2
            Cancelled, //3
        }
    }
}
