using AutoMapper;
using BCrypt.Net;
using Books.Application.DTOs.UserDTOs;
using Books.Application.Interfaces.Repositories;
using Books.Application.Interfaces.Services;
using Books.Domain.Entities;

namespace Books.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserReadDto> CreateUserAsync(UserCreateDto dto)
    {
        var entity = _mapper.Map<UserEntity>(dto);

        entity.PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(dto.Password);

        var created = await _userRepository.AddUserAsync(entity);

        return _mapper.Map<UserReadDto>(created);
    }

    public async Task<ICollection<UserReadDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllUserAsync();
        return _mapper.Map<ICollection<UserReadDto>>(users);
    }

    public async Task<UserReadDto?> GetByEmailAsync(string email)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user == null) return null;

        return _mapper.Map<UserReadDto>(user);
    }
}