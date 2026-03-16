using Books.Api.Exceptions;
using Books.Application.Interfaces.Helpers;
using Books.Application.Interfaces.Repositories;
using Books.Application.Interfaces.Services;
using Books.Application.Mapping;
using Books.Application.Services;
using Books.Infrastructure.Data;
using Books.Infrastructure.Helpers;
using Books.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Books.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //builder.Services.AddDbContext<LibraryDbContext>(options =>
            //{
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            //});
            builder.Services.AddDbContext<LibraryDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
               builder.Services.AddAutoMapper(
               _ => { }, //пустий конфігураційний делегат.
               typeof(BookProfile).Assembly,
               typeof(AuthorProfile).Assembly,
               typeof(GenreProfile).Assembly,
               typeof(UserProfile).Assembly
            );
            // Add services to the container.
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IAuthorService, AuthorService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IHashHelper, HashHelper>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddProblemDetails();

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseExceptionHandler();


            app.MapControllers();

            app.Run();
        }
    }
}
