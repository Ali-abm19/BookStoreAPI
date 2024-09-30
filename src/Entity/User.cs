using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.src.Entity
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public string? Address { get; set; }

        public long? Phone { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public byte[]? Salt { get; set; }

        public Role Role { get; set; } = Role.Customer;

        // connnections with other entities

        public Guid? OrderId { get; set; }
        public List<Order>? Order { get; set; }

        public Guid? CartId { get; set; }
       // public Cart? Cart { get; set; }
    }

    public enum Role
    {
        Admin,
        Customer,
    }
}
