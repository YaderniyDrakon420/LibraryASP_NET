using Books.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Books.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenreController : ControllerBase
{
    private readonly IGenreService _genreService;

    public GenreController(IGenreService genreService)
    {
        _genreService = genreService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var genres = await _genreService.GetAllGenresAsync();
        return Ok(genres);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGenreById([FromRoute] int id)
    {
        var genre = await _genreService.GetGenreByIdAsync(id);
        return Ok(genre);
    }

    [HttpPost]
    public async Task<IActionResult> AddGenre([FromBody] Books.Application.DTOs.GenreDTOs.GenreCreateDto genreDto)
    {
        int? id = await _genreService.CreateGenreAsync(genreDto);
        if (id != null)
        {
            return CreatedAtAction(nameof(GetGenreById), new { id }, id);
        }
        return BadRequest();
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchGenres([FromQuery] string query)
    {
        var genres = await _genreService.GetAllGenresAsync();
        var result = genres.Where(g => g.Title.Contains(query, StringComparison.OrdinalIgnoreCase));
        return Ok(result);
    }
}