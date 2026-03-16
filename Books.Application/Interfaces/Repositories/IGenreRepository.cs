using Books.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Books.Application.Interfaces.Repositories;

public interface IGenreRepository
{
    Task<ICollection<GenreEntity>> GetAllGenreAsync();
    Task<GenreEntity?> GetGenreByIdAsync(int id);
    Task<int?> AddGenreAsync(GenreEntity genre);
    Task<bool> UpdateGenreAsync(GenreEntity genre);
    Task<bool> DeleteGenreAsync(int id);
}