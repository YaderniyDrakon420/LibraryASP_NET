using AutoMapper;
using Books.Application.DTOs.AuthorDTOs;
using Books.Application.Interfaces.Repositories;
using Books.Application.Interfaces.Services;
using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Services;

public class AuthorService : IAuthorService
{
    private readonly IMapper _mapper;
    private readonly IAuthorRepository _authorRepository;
    public AuthorService(IMapper mapper, IAuthorRepository authorRepository)
    {
        _mapper = mapper;
        _authorRepository = authorRepository;
    }
    public async Task<int?> CreateAuthorAsync(AuthorCreateDto dto)
    {
        var authorEntity = _mapper.Map<AuthorEntity>(dto);
        return await _authorRepository.AddAuthorAsync(authorEntity);
    }

    public async Task<ICollection<AuthorReadDto>> GetAllAuthorsAsync()
    {
        var authors = await _authorRepository.GetAllAuthorsAsync();
        return _mapper.Map<ICollection<AuthorReadDto>>(authors);
        
    }

    public async Task<AuthorReadDto?> GetAuthorByIdAsync(int id)
    {
        return _mapper.Map<AuthorReadDto>(await _authorRepository.GetAuthorByIdAsync(id));
    }
}
