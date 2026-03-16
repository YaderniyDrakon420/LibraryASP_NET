using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Interfaces.Repositories;

public interface IGenreRepository
{
    Task<ICollection<GenreEntity>> GetAllGenreAsync();
    Task<GenreEntity?> GetGenreByIdAsync(int id);
    Task<int?> AddGenreAsync(GenreEntity author);
}
