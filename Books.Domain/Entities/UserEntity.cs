using Books.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Domain.Entities;

public class UserEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string PasswordHash { get; set; }

    public UserRole Role { get; set; } = UserRole.User;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsActive { get; set; } = true;
}
