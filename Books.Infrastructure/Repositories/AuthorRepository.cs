using Books.Application.Interfaces.Repositories;
using Books.Domain.Entities;
using Books.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Books.Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly LibraryDbContext _context;
    public AuthorRepository(LibraryDbContext context)
    {
        _context = context;
    }
    public async Task<int?> AddAuthorAsync(AuthorEntity author)
    {
        await _context.Authors.AddAsync(author);
        await _context.SaveChangesAsync();
        return author.Id;

    }

    public async Task<ICollection<AuthorEntity?>> GetAllAuthorsAsync()
    {
       return await _context.Authors.ToListAsync();
    }

    public async Task<AuthorEntity?> GetAuthorByIdAsync(int id)
    {
      return await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<bool> UpdateAuthorAsync(AuthorEntity author)
    {
        var existingAuthor = await _context.Authors.FirstOrDefaultAsync(a => a.Id == author.Id);

        if (existingAuthor == null)
            return false;

        existingAuthor.Name = author.Name;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAuthorAsync(int id)
    {
        var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);

        if (author == null)
            return false;

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();

        return true;
    }
}
