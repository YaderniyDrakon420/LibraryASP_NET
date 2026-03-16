using Books.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.DTOs.UserDTOs;

/// <summary>
/// Реэстрація користувача
/// </summary>
public  class UserCreateDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    public string Password { get; set; }
}
