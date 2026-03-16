using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Interfaces.Repositories;

public interface IUserRepository
{
    /// <summary>
    /// Повертає email адрес
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<string> AddUserAsync(UserEntity user, string password);
    Task<ICollection<UserEntity>> GetAllUserAsync();
    Task<UserEntity?> GetUserByEmailAsync(string email);
}
