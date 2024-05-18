using System.Collections.Generic;
using System.Threading.Tasks;
using BookStoreApi.DTOs;

namespace BookStoreApi.Interfaces;

public interface IBookRepository
{
    Task<List<BookDTO>> GetAllBooksAsync();
    Task<BookDTO> AddBookAsync(BookDTO bookDto);
}