using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using BookStoreApi.Data;
using BookStoreApi.Interfaces;
using BookStoreApi.Repositories;
using BookStoreApi.Services;
using BookStoreApi.Middleware;

namespace BookStoreApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            services.AddControllers();

            // Add DbContext with SQL Server support
            //services.AddDbContext<BooksContext>(options =>
             //   options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Configure Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Book Store API", Version = "v1" });
            });

            // Dependency Injection for services and repositories
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IBookRepository, BookRepository>();

            // Add CORS policy
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder =>
                    builder.WithOrigins("http://example.com") // Specify the allowed origin
                           .AllowAnyMethod()
                           .AllowAnyHeader());
            });

            // Authentication and Authorization
            services.AddAuthentication(); // Setup as per JwtSettings or other configurations
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Book Store API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("AllowSpecificOrigin");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Custom middleware for logging HTTP requests and responses
            app.UseCustomLogging();
        }
    }
}
