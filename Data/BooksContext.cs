using Microsoft.EntityFrameworkCore;
using BookStoreApi.Models;

namespace BookStoreApi.Data;

public class BooksContext : DbContext
{
    public BooksContext(DbContextOptions<BooksContext> options) : base(options) { }

    public DbSet<Book> Books { get; set; }
}
