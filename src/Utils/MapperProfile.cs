using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.src.Entity;
using static BookStore.src.DTO.OrderDTO;

namespace BookStore.src.Utils
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //Order class
            CreateMap<Order, OrderReadDto>();
            CreateMap<OrderCreateDto, Order>();
            CreateMap<OrderUpdateDto, Order>().
            ForAllMembers(opts => opts.Condition((src, dest, srcProperty) => srcProperty != null));

        }

    }
}