using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Domain.Entities;

public class BookEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Year { get; set; }

    public int GenreId { get; set; }
    public GenreEntity? Genre { get; set; }
    public ICollection<AuthorEntity>? Authors { get; set; }
    public DateTime? CreatedAt { get; }
}
