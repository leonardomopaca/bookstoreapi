using System.Collections.Generic;
using System.Threading.Tasks;
using BookStoreApi.Models;
using BookStoreApi.Interfaces;
using BookStoreApi.DTOs;

namespace BookStoreApi.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<List<BookDTO>> GetAllBooksAsync()
        {
            return await _bookRepository.GetAllBooksAsync();
        }

        public async Task<BookDTO> AddBookAsync(BookDTO bookDto)
        {
            return await _bookRepository.AddBookAsync(bookDto);
        }
    }
}
