using Books.Application;
using Books.Application.DTOs.BookDTOs;
using Books.Application.Interfaces.Repositories;
using Books.Domain.Entities;
using Books.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly LibraryDbContext _context;
    public BookRepository(LibraryDbContext context)
    {
        _context = context;
    }

    private async Task ValidateGenreAsync(int genreId)
    {
        if (!await _context.Genres.AnyAsync(g => g.Id == genreId))
            throw new Exception("Genre not found");
    }

    private async Task<ICollection<AuthorEntity>> GetAuthorsAsync(ICollection<int> authorIds)
    {
        var authors = await _context.Authors.Where(a => authorIds.Contains(a.Id)).ToListAsync();
        if (authors.Count != authorIds.Count)
            throw new Exception("Some authors not found");
        return authors;
    }
    public async Task<int?> AddBookAsync(BookEntity book, ICollection<int>? authorIds)
    {
        await ValidateGenreAsync(book.GenreId);

        if (authorIds != null)
            book.Authors = await GetAuthorsAsync(authorIds);

        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book.Id;
    }




    public async Task<ICollection<BookEntity>> GetAllBooksAsync()
    {
        return await _context.Books
            .Include(b => b.Authors)
            .ToListAsync();
    }

    public async Task<BookEntity> GetBookByIdAsync(int id)
    {
        return await _context.Books.Include(b => b.Authors).SingleOrDefaultAsync(b => b.Id == id);
    }
}
