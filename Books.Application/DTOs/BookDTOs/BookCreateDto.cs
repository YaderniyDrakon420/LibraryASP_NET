using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.DTOs.BookDTOs;

public class BookCreateDto
{
    public string Title { get; set; } = string.Empty;
    public int Year { get; set; }
    public int GenreId { get; set; }
    public ICollection<int>? AuthorsId { get; set; }
}

