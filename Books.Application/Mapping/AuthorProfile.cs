using System.Linq;
using AutoMapper;
using Books.Application.DTOs.AuthorDTOs;
using Books.Domain.Entities;

namespace Books.Application.Mapping;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        CreateMap<AuthorCreateDto, AuthorEntity>();

        CreateMap<AuthorEntity, AuthorReadDto>()
            .ForMember(
                dest => dest.BooksId,
                opt => opt.MapFrom(src => src.BookAuthors.Select(ba => ba.BookId))
            );
    }
}