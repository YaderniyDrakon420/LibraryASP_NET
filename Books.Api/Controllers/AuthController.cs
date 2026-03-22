using Books.Application.DTOs.UserDTOs;
using Books.Application.Interfaces.Services;
using Books.Domain.Entities;
using Books.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Books.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService; 
    private readonly LibraryDbContext _context;

    public AuthController(IUserService userService, IJwtService jwtService, LibraryDbContext context)
    {
        _userService = userService;
        _jwtService = jwtService;
        _context = context;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
    {
        var user = await _context.Users
    .Include(u => u.RefreshTokens)
    .FirstOrDefaultAsync(u => u.Email == dto.Email);

        var accessToken = _jwtService.GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken(Request.HttpContext.Connection.RemoteIpAddress?.ToString());
        refreshToken.UserId = user.Id;

        user.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync();

        SetRefreshTokenCookie(refreshToken);

        return Ok(new { accessToken });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserCreateDto dto)
    {
        var user = await _userService.CreateUserAsync(dto);
        if (user == null) return BadRequest();

        return CreatedAtAction(null, new { email = user.Email }, user);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh()
    {
        var token = Request.Cookies["refreshToken"];
        if (string.IsNullOrEmpty(token)) return Unauthorized();

        var refreshToken = await _context.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == token);

        if (refreshToken == null || refreshToken.IsRevoked || refreshToken.Expires < DateTime.UtcNow)
            return Unauthorized();

        var user = refreshToken.User;
        var newAccessToken = _jwtService.GenerateAccessToken(user);
        var newRefreshToken = GenerateRefreshToken(Request.HttpContext.Connection.RemoteIpAddress?.ToString());
        newRefreshToken.UserId = user.Id;

        refreshToken.IsRevoked = true;
        user.RefreshTokens.Add(newRefreshToken);
        await _context.SaveChangesAsync();

        SetRefreshTokenCookie(newRefreshToken);
        return Ok(new { accessToken = newAccessToken });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var token = Request.Cookies["refreshToken"];
        if (string.IsNullOrEmpty(token)) return BadRequest();

        var refreshToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == token);

        if (refreshToken != null)
        {
            refreshToken.IsRevoked = true;
            await _context.SaveChangesAsync();
        }

        Response.Cookies.Delete("refreshToken");
        return Ok();
    }

    private RefreshToken GenerateRefreshToken(string ipAddress)
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);

        return new RefreshToken
        {
            Token = Convert.ToBase64String(randomBytes),
            Expires = DateTime.UtcNow.AddDays(7),
            Created = DateTime.UtcNow,
            IpAddress = ipAddress
        };
    }

    private void SetRefreshTokenCookie(RefreshToken refreshToken)
    {
        Response.Cookies.Append("refreshToken", refreshToken.Token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = refreshToken.Expires
        });
    }
}
