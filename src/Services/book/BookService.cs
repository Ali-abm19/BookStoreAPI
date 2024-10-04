using System;
using AutoMapper;
using BookStore.src.Entity;
using BookStore.src.Repository;
using BookStore.src.Services.book;
using BookStore.src.Services.category;
using BookStore.src.Utils;
using static BookStore.src.DTO.BookDTO;

namespace BookStore.Services.book
{
    public class BookService : IBookService
    {
        protected readonly BookRepository _BookRepository;
        protected readonly IMapper _mapper;
        protected readonly CategoryRepository _categoryRepo;

        public BookService(
            BookRepository bookRepository,
            IMapper mapper,
            CategoryRepository categoryRepo
        )
        {
            _BookRepository = bookRepository;
            _mapper = mapper;
            _categoryRepo = categoryRepo;
        }

        public async Task<ReadBookDto> CreateOneAsync(CreateBookDto createDto)
        {
            Book b = _mapper.Map<CreateBookDto, Book>(createDto); //convert CreateBookDto to Book

            //update the category of the Book using the given CategroyName in the CreateBookDto
            var categoryOfTheBook = _categoryRepo
                .GetAllAsync()
                .Result.FirstOrDefault(c => c.CategoryName == createDto.CategoryName);
            if (categoryOfTheBook != null)
            {
                b.Category = categoryOfTheBook;
                b.CategoryId = categoryOfTheBook.CategoryId;
            }
            else
            {
                throw new Exception("Book addition failed");
            }
            Book created = await _BookRepository.CreateOneAsync(b);
            // end of Category update
            return _mapper.Map<Book, ReadBookDto>(created); //return type: ReadBookDto
        }

        public async Task<ReadBookDto> GetBookByIdAsync(Guid id)
        {
            var b = await _BookRepository.GetBookByIdAsync(id);
            if (b == null)
            {
                throw CustomException.NotFound($"Book {id} deoes not exist ");
            }
            var dto = _mapper.Map<Book, ReadBookDto>(b);
            return dto;
        }

        public async Task<bool> DeleteOneAsync(Guid id)
        {
            var bookToDelete = await _BookRepository.GetBookByIdAsync(id);
            bool result = await _BookRepository.DeleteOneAsync(bookToDelete);

            if (result == false)
            {
                throw CustomException.BadRequest($"Book {id} was not deleted ");
            }
            else
                return result;
        }

        public async Task<List<ReadBookDto>> GetAllAsync(PaginationOptions paginationOptions)
        {
            var books = await _BookRepository.GetAllAsync(paginationOptions);
            var dtos = _mapper.Map<List<Book>, List<ReadBookDto>>(books);
            return dtos;
        }

        public async Task<List<ReadBookDto>> GetAllAsyncWithConditions()
        {
            var books = await _BookRepository.GetAllAsyncWithConditions();
            var dtos = _mapper.Map<List<Book>, List<ReadBookDto>>(books);
            return dtos;
        }

        public async Task<bool> UpdateOneAsync(Guid id, UpdateBookDto updateDto)
        {
            var bookToUpdate = await _BookRepository.GetBookByIdAsync(id);
            if (bookToUpdate == null)
            {
                throw CustomException.NotFound($"Book {id} was not Found. Update faild ");
            }
            _mapper.Map(updateDto, bookToUpdate);
            return await _BookRepository.UpdateOneAsync(bookToUpdate);
        }
    }
}
