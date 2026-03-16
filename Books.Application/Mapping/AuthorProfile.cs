using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Books.Application.DTOs.AuthorDTOs;
using Books.Domain.Entities;

namespace Books.Application.Mapping;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        //AuthoCreateDto - src, AuthorEntity - dest
        CreateMap<AuthorCreateDto, AuthorEntity>();

        CreateMap<AuthorEntity, AuthorReadDto>()
           .ForMember(dest => dest.BooksId, opt => opt.MapFrom(src => src.Books.Select(b => b.Id)));
    }
}
