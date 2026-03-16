using Books.Application.Interfaces.Helpers;
using Books.Application.Interfaces.Repositories;
using Books.Domain.Entities;
using Books.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Infrastructure.Repositories;

public class UserRepository:IUserRepository
{
    private readonly LibraryDbContext _context;
    private readonly IHashHelper _helper;

    public UserRepository(LibraryDbContext context, IHashHelper helper)
    {
        _context = context;
        _helper = helper;
    }
    public async Task<string> AddUserAsync(UserEntity user, string password)
    {
        user.PasswordHash = _helper.Hash(password);
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user.Email;
    }

    public async Task<ICollection<UserEntity>> GetAllUserAsync()
    {
        return await _context.Users.ToArrayAsync();
    }
    public async Task<UserEntity?> GetUserByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }
}
