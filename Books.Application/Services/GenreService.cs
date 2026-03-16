using Books.Application.DTOs.GenreDTOs;
using Books.Application.Interfaces.Repositories;
using Books.Application.Interfaces.Services;
using Books.Domain.Entities;

namespace Books.Application.Services;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _genreRepository;

    public GenreService(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<int?> CreateGenreAsync(GenreCreateDto dto)
    {
        var genre = new GenreEntity
        {
            Title = dto.Title
        };
        await _genreRepository.AddAsync(genre);
        return genre.Id;
    }

    public async Task<GenreReadDto?> GetGenreByIdAsync(int id)
    {
        var genre = await _genreRepository.GetByIdAsync(id);
        if (genre == null) return null;

        return new GenreReadDto
        {
            Id = genre.Id,
            Title = genre.Title,
            BooksId = genre.Books?.Select(b => b.Id).ToList()
        };
    }

    public async Task<ICollection<GenreReadDto>> GetAllGenresAsync()
    {
        var genres = await _genreRepository.GetAllAsync();
        return genres.Select(g => new GenreReadDto
        {
            Id = g.Id,
            Title = g.Title,
            BooksId = g.Books?.Select(b => b.Id).ToList()
        }).ToList();
    }
}