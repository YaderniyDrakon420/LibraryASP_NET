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
}
