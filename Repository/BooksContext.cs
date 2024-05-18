using BookStoreApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Repository;

public class BooksContext : DbContext
{
    public BooksContext(DbContextOptions<BooksContext> options) : base(options) { }

    public DbSet<Book> Books { get; set; }
}
