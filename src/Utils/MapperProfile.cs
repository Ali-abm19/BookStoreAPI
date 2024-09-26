using AutoMapper;
using BookStore.src.Entity;
using static BookStore.src.DTO.BookDTO;

namespace BookStore.src.Utils
{
    public class MapperProfile : Profile
    {

        /*
         CreateMap<Book, ReadBookDto>();
         CreateMap<CreateBookDto, Book>();
         CreateMap<UpdateBookDto, Book>().
         ForAllMembers(opts => opts.Condition((src, dest, srcProperty) => srcProperty != null));
         */
    }
}
