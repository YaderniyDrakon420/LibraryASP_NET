using Books.Application.DTOs.AuthorDTOs;
using Books.Application.DTOs.BookDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Interfaces.Services;

public interface IAuthorService
{
    /// <summary>
    /// Створює нового автора
    /// </summary>
    /// <param name="dto">DTO для створення автора</param>
    /// <returns>Id створеного автора</returns>
    Task<int?> CreateAuthorAsync(AuthorCreateDto dto);

    /// <summary>
    /// Повертає автора по Id
    /// </summary>
    /// <param name="id">Id автора</param>
    /// <returns>AuthorReadDto або null, якщо не знайдено</returns>
    Task<AuthorReadDto?> GetAuthorByIdAsync(int id);

    /// <summary>
    /// Повертає всіx авторів
    /// </summary>
    /// <returns>Колекція AuthorReadDto</returns>
    Task<ICollection<AuthorReadDto>> GetAllAuthorsAsync();
}
