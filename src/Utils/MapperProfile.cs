using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.src.Entity;
using BookStore.src.DTO;
using AutoMapper;

namespace BookStore.src.Utils
{
    public class MapperProfile : Profile
    {
         public MapperProfile(){

        // Mapping between Cart entity and DTOs
        CreateMap<Cart, CartDTO.CartReadDto>();
        CreateMap<CartDTO.CartCreateDto, Cart>();
        CreateMap<CartDTO.CartUpdateDto, Cart>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcProperty) => srcProperty != null));
    }
         }
    }
