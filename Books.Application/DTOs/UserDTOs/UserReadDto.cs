using Books.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.DTOs.UserDTOs;

public class UserReadDto
{
    public string Email { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
}
