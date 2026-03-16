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
    public async Task<IActionResult> AddBook([FromBody] BookCreateDto bookDto)
    {
        int? id = await _bookService.CreateBookAsync(bookDto);
        if (id != null)
        {
            return CreatedAtAction(nameof(GetBookById), new { id}, id);
        }
        else
        {
            return BadRequest();
        }
    }


}
