using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Domain.Entities;

public class BookAuthorEntity
{
    public int BookId { get; set; }
    public BookEntity Book { get; set; }

    public int AuthorId { get; set; }
    public AuthorEntity Author { get; set; }
}
