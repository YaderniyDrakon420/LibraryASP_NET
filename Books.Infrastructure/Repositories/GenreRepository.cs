using Books.Application.Interfaces.Repositories;
using Books.Domain.Entities;
using Books.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Books.Infrastructure.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly LibraryDbContext _context;

    public GenreRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<int?> AddGenreAsync(GenreEntity genre)
    {
        await _context.Genres.AddAsync(genre);
        await _context.SaveChangesAsync();
        return genre.Id;
    }

    public async Task<ICollection<GenreEntity>> GetAllGenreAsync()
    {
        return await _context.Genres.ToListAsync();
    }

    public async Task<GenreEntity?> GetGenreByIdAsync(int id)
    {
        return await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<bool> UpdateGenreAsync(GenreEntity genre)
    {
        var existingGenre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == genre.Id);

        if (existingGenre == null)
            return false;

        existingGenre.Title = genre.Title;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteGenreAsync(int id)
    {
        var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);

        if (genre == null)
            return false;

        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync();

        return true;
    }
}