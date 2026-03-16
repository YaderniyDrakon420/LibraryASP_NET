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

    public async Task AddAsync(GenreEntity genre)
    {
        await _context.Genres.AddAsync(genre);
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<GenreEntity>> GetAllAsync()
    {
        return await _context.Genres.Include(g => g.Books).ToListAsync();
    }

    public async Task<GenreEntity?> GetByIdAsync(int id)
    {
        return await _context.Genres.Include(g => g.Books)
                                    .FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<bool> UpdateAsync(GenreEntity genre)
    {
        var existing = await _context.Genres.FirstOrDefaultAsync(g => g.Id == genre.Id);
        if (existing == null) return false;

        existing.Title = genre.Title;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);
        if (genre == null) return false;

        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync();
        return true;
    }
}