using Books.Application.DTOs.BookDTOs;
using Books.Application.DTOs.GenreDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Interfaces.Services;

public interface IGenreService
{
    
    Task<int?> CreateGenreAsync(GenreCreateDto dto);
    Task<BookReadDto?> GetGenreByIdAsync(int id);
    Task<ICollection<GenreReadDto>> GetAllGenresAsync();
}
