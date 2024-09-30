using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.src.Entity;

namespace BookStore.src.DTO
{
    public class UserDTO
    {
        public class UserCreateDto
        {
            public string? Name { get; set; }

            public string? Address { get; set; }

            public long? Phone { get; set; }

            public string Email { get; set; }

            public string Password { get; set; }
        }

        public class UserReadDto
        {
            public Guid Id { get; set; }
            public string? Name { get; set; }

            public string? Address { get; set; }

            public long? Phone { get; set; }

            public string Email { get; set; }

            public Role Role { get; set; }
        }

        public class UserUpdateDto
        {
            public string? Name { get; set; }

            public string? Address { get; set; }

            public long? Phone { get; set; }

            public string Email { get; set; }

            public string Password { get; set; }

            public byte[]? Salt { get; set; }
        }
    }
}
