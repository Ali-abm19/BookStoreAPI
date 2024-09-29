using System;
using AutoMapper;
using BookStore.src.Entity;
using BookStore.src.Repository;
using BookStore.src.Services.book;
using static BookStore.src.DTO.BookDTO;

namespace BookStore.Services.book
{
    public class BookService : IBookService
    {
        protected readonly BookRepository _BookRepository;
        protected readonly IMapper _mapper;

        public BookService(BookRepository bookRepository, IMapper mapper)
        {
            _BookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<ReadBookDto> CreateOneAsync(CreateBookDto createDto)
        {
            Book b = _mapper.Map<CreateBookDto, Book>(createDto); //convert CreateBookDto to Book
            Book created = await _BookRepository.CreateOneAsync(b);
            return _mapper.Map<Book, ReadBookDto>(created); //return type: ReadBookDto
        }

        public async Task<ReadBookDto> GetBookByIdAsync(Guid id)
        {
            var b = await _BookRepository.GetBookByIdAsync(id);
            var dto = _mapper.Map<Book, ReadBookDto>(b);
            return dto;
        }

        public async Task<bool> DeleteOneAsync(Guid id)
        {
            var bookToDelete = await GetBookByIdAsync(id);
            return await _BookRepository.DeleteOneAsync(
                _mapper.Map<ReadBookDto, Book>(bookToDelete)
            );
        }

        public async Task<List<ReadBookDto>> GetAllAsync()
        {
            var books = await _BookRepository.GetAllAsync();
            var dtos = _mapper.Map<List<Book>, List<ReadBookDto>>(books);
            return dtos;
        }

        public async Task<bool> UpdateOneAsync(Guid id, UpdateBookDto updateDto)
        {
            var bookToUpdate = await GetBookByIdAsync(id);
            if (bookToUpdate == null)
            {
                return false;
            }
            _mapper.Map(updateDto, bookToUpdate);
            return await _BookRepository.UpdateOneAsync(
                _mapper.Map<ReadBookDto, Book>(bookToUpdate)
            );
        }
    }
}