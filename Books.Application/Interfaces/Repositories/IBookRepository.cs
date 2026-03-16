using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books.Application.DTOs.BookDTOs;
using Books.Domain;
using Books.Domain.Entities;

namespace Books.Application.Interfaces.Repositories;

public interface IBookRepository
{
    /// <summary>
    /// Отримання книг з БД всіх
    /// </summary>
    /// <returns></returns>
    Task<ICollection<BookEntity>> GetAllBooksAsync();
    Task<BookEntity> GetBookByIdAsync(int id);
    Task<int?> AddBookAsync(BookEntity book, ICollection<int>? authorIds);

}
