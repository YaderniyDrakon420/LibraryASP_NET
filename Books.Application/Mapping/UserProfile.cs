using AutoMapper;
using Books.Application.DTOs.UserDTOs;
using Books.Domain.Entities;

namespace Books.Application.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserCreateDto, UserEntity>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

        CreateMap<UserEntity, UserReadDto>()
            .ForMember(dest => dest.Role,
                       opt => opt.MapFrom(src => src.Role.ToString()));
    }
}
