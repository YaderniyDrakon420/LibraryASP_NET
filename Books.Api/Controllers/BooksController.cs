using Books.Application.DTOs.BookDTOs;
using Books.Application.Interfaces.Services;
using Books.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Books.Api.Controllers;

[ApiController]
[Route("api/[controller]")] //https://localhost:PORT/api/books
public class BooksController(IBookService _bookService):ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var books = await _bookService.GetAllBooksAsync();
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById([FromRoute] int id)
    {
        var book = await _bookService.GetBookByIdAsync(id);
        return Ok(book);
    }


    [HttpPost]
    public async Task<IActionResult> AddBook([FromForm] BookCreateDto bookDto)
    {
        int? id = await _bookService.CreateBookAsync(bookDto);

        if (id != null)
        {
            return CreatedAtAction(nameof(GetBookById), new { id }, id);
        }

        return BadRequest();
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchBooks(
    [FromQuery] int? authorId,
    [FromQuery] int? genreId,
    [FromQuery] int? year,
    [FromQuery] string? title)
    {
        var books = await _bookService.GetAllBooksAsync();

        if (authorId != null)
            books = books.Where(b => b.AuthorsId.Contains(authorId.Value)).ToList();

        if (genreId != null)
            books = books.Where(b => b.GenreId == genreId.Value).ToList();

        if (year != null)
            books = books.Where(b => b.Year == year.Value).ToList();

        if (!string.IsNullOrWhiteSpace(title))
            books = books.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();

        return Ok(books);
    }


}
