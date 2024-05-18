using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreApi.Data;
using BookStoreApi.DTOs;
using BookStoreApi.Interfaces;
using BookStoreApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Repositories;

public class BookRepository : IBookRepository
{
    private readonly BooksContext _context;

    public BookRepository(BooksContext context)
    {
        _context = context;
    }

    public async Task<List<BookDTO>> GetAllBooksAsync()
    {
        return await _context.Books
            .Select(b => new BookDTO { Id = b.Id, Title = b.Title, Author = b.Author, Year = b.Year })
            .ToListAsync();
    }

    public async Task<BookDTO> AddBookAsync(BookDTO bookDto)
    {
        var book = new Book { Title = bookDto.Title, Author = bookDto.Author, Year = bookDto.Year };
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        bookDto.Id = book.Id;
        return bookDto;
    }
}