using AutoMapper;
using Books.Application.DTOs.UserDTOs;
using Books.Application.Interfaces.Repositories;
using Books.Application.Interfaces.Services;
using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public async Task<string> CreateUserAsync(UserCreateDto dto)
    {
        var entity = _mapper.Map<UserEntity>(dto);
        return await _userRepository.AddUserAsync(entity, dto.Password);
    }

    public async Task<ICollection<UserReadDto>> GetAllUserAsync()
    {
        var users = await _userRepository.GetAllUserAsync();
        return _mapper.Map<ICollection<UserReadDto>>(users);
    }
    public async Task<UserReadDto?> GetByEmailUserAsync(string email)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user == null) return null;

        return _mapper.Map<UserReadDto>(user);
    }
}
