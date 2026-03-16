using AutoMapper;
using Books.Application.DTOs.BookDTOs;
using Books.Application.Interfaces.Repositories;
using Books.Application.Interfaces.Services;
using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;
    private readonly IMapper _mapper;

    public BookService(IBookRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    // Створення книги
    public async Task<int?> CreateBookAsync(BookCreateDto dto)
    {
        var book = _mapper.Map<BookEntity>(dto);
        return await _repository.AddBookAsync(book, dto.AuthorsId);
    }

    // Отримати книгу по Id
    public async Task<BookReadDto?> GetBookByIdAsync(int id)
    {
        var book = await _repository.GetBookByIdAsync(id);
        if (book == null) return null;
        var dto = _mapper.Map<BookReadDto>(book);
        return dto;
    }

    // Отримати всі книги
    public async Task<ICollection<BookReadDto>> GetAllBooksAsync()
    {
        var books = await _repository.GetAllBooksAsync();
        return _mapper.Map<ICollection<BookReadDto>>(books);
    }

}
