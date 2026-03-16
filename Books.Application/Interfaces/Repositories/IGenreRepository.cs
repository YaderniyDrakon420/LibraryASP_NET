using Books.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Books.Application.Interfaces.Repositories;

public interface IGenreRepository
{
    Task AddAsync(GenreEntity genre);
    Task<GenreEntity?> GetByIdAsync(int id);
    Task<ICollection<GenreEntity>> GetAllAsync();
}