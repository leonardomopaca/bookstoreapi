// File: Tests/BookStoreApi.Tests/Controllers/BooksControllerTests.cs

using Xunit;
using Moq;
using BookStoreApi.Controllers;
using BookStoreApi.Services;
using BookStoreApi.Interfaces;
using BookStoreApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreApi.Tests.Controllers
{
    public class BooksControllerTests
    {
        private readonly Mock<IBookService> _mockService;
        private readonly BooksController _controller;

        public BooksControllerTests()
        {
            _mockService = new Mock<IBookService>();
            //_controller = new BooksController(_mockService.Object);
        }

        [Fact]
        public async Task GetAllBooks_ReturnsOkObjectResult_WithAListOfBooks()
        {
            // Arrange
            var books = new List<BookDTO>
            {
                new BookDTO { Id = 1, Title = "Book One", Author = "Author A", Year = 2021 },
                new BookDTO { Id = 2, Title = "Book Two", Author = "Author B", Year = 2022 }
            };
            _mockService.Setup(s => s.GetAllBooksAsync()).ReturnsAsync(books);

            // Act
            var result = await _controller.GetAllBooks();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<BookDTO>>(actionResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task AddBook_ReturnsCreatedAtActionResult_WithBook()
        {
            // Arrange
            var newBook = new BookDTO { Title = "New Book", Author = "New Author", Year = 2023 };
            _mockService.Setup(s => s.AddBookAsync(It.IsAny<BookDTO>())).ReturnsAsync(new BookDTO { Id = 3, Title = "New Book", Author = "New Author", Year = 2023 });

            // Act
            var result = await _controller.AddBook(newBook);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<BookDTO>(actionResult.Value);
            Assert.Equal("New Book", returnValue.Title);
        }
    }
}
