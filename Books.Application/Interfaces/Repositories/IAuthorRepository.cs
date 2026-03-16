using Books.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Books.Application.Interfaces.Repositories;

public interface IAuthorRepository
{
    Task<ICollection<AuthorEntity?>> GetAllAuthorsAsync();
    Task<AuthorEntity?> GetAuthorByIdAsync(int id);
    Task<int?> AddAuthorAsync(AuthorEntity author);
    Task<bool> UpdateAuthorAsync(AuthorEntity author);
    Task<bool> DeleteAuthorAsync(int id);
}