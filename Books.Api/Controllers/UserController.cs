using Books.Application.DTOs.UserDTOs;
using Books.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Books.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService _userService):ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUserAsync();
        return Ok(users);
    }

    [HttpGet("{email}")]
    public async Task<IActionResult> GetUserByEmail([FromRoute] string email)
    {
        var user = await _userService.GetByEmailUserAsync(email);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserCreateDto dto)
    {
        var email = await _userService.CreateUserAsync(dto);

        if (email != null)
        {
            return CreatedAtAction(nameof(GetUserByEmail), new { email }, email);
        }
        else
        {
            return BadRequest();
        }
    }
}
