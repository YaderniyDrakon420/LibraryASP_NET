using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Domain.Entities;

public class GenreEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public ICollection<BookEntity>? Books { get; set; }
}
