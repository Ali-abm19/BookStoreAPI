using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.src.Entity;
using BookStore.src.DTO;
using AutoMapper;
using BookStore.src.Entity;
using static BookStore.src.DTO.BookDTO;
using static BookStore.src.DTO.CategoryDTO;
using static BookStore.src.DTO.OrderDTO;
using static BookStore.src.DTO.UserDTO;

namespace BookStore.src.Utils
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Book, ReadBookDto>();
            CreateMap<CreateBookDto, Book>();
            CreateMap<UpdateBookDto, Book>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcProperty) => srcProperty != null)
                );
            //Order class
            CreateMap<Order, OrderReadDto>();
            CreateMap<OrderCreateDto, Order>();
            CreateMap<OrderUpdateDto, Order>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcProperty) => srcProperty != null)
                );

            CreateMap<User, UserReadDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcProperty) => srcProperty != null)
                );

            CreateMap<Category, CategoryReadDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateNameDto, Category>();
            CreateMap<CategoryUpdateDesDto, Category>()
                .
                //condtion for convert
                ForAllMembers(opts =>
                    opts.Condition((src, dest, srcProperty) => srcProperty != null)
                );
        }
    }
}

         public MapperProfile(){

        // Mapping between Cart entity and DTOs
        CreateMap<Cart, CartDTO.CartReadDto>();
        CreateMap<CartDTO.CartCreateDto, Cart>();
        CreateMap<CartDTO.CartUpdateDto, Cart>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcProperty) => srcProperty != null));
    }
         }
    }
