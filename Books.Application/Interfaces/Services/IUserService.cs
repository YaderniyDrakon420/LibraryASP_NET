using Books.Application.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Interfaces.Services;

public interface IUserService
{
    /// <summary>
    /// повертає email адрес
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<string> CreateUserAsync(UserCreateDto dto);
    Task<ICollection<UserReadDto>> GetAllUserAsync();
    Task<UserReadDto?> GetByEmailUserAsync(string email);
}
