using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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

        //public String log //<- wish i have time to add this :( @ali 10/4/2024 11PM

        // connnections with other entities
        public Guid UserId { get; set; } //handled in the repo @ali
        //public User User { get; set; } // handled in the repo @ali 
        //actually, I decided we don't need the whole User object.

        // public Guid CartId { get; set; }
        public Guid CartId { get; set; } //i wamt list from the cat 1:21 more than book

        //Manar used cartItems below.
        //this is redundent logic for cart. we should just use cart @ali
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
