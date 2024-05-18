using Microsoft.AspNetCore.Mvc;
using BookStoreApi.Services;
using BookStoreApi.DTOs;
using System.Threading.Tasks;

namespace BookStoreApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly Interfaces.IBookService _bookService;

        public BooksController(Interfaces.IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BookDTO bookDto)
        {
            var book = await _bookService.AddBookAsync(bookDto);
            if (book == null)
                return BadRequest();

            return CreatedAtAction(nameof(GetAllBooks), new { id = book.Id }, book);
        }
    }
}
