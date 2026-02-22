using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Application.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using AuthenticationService.Infrastructure.Data;
using AuthenticationService.Domain.Entities;
using AuthenticationService.Application.Services;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IJwtService _jwtService;
    private readonly IPasswordService _passwordService;
    private readonly IMapper _mapper;

    public AuthController(
        AppDbContext context,
        IJwtService jwtService,
        IPasswordService passwordService,
        IMapper mapper)
    {
        _context = context;
        _jwtService = jwtService;
        _passwordService = passwordService;
        _mapper = mapper;
    }

    /* ⭐ REGISTER */
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        if (await _context.Users.AnyAsync(x => x.Email == request.Email))
            return BadRequest("User already exists");

        var user = _mapper.Map<User>(request);

        user.PasswordHash = _passwordService.HashPassword(request.Password);
        user.Role = request.Role ?? "User";
        user.CreatedDate = DateTime.UtcNow;

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok("User Registered Successfully");
    }

    /* ⭐ LOGIN + REFRESH TOKEN GENERATION */
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Email == request.Email);

        if (user == null)
            return Unauthorized("Invalid credentials");

        if (!_passwordService.VerifyPassword(request.Password, user.PasswordHash))
            return Unauthorized("Invalid credentials");

        var accessToken = _jwtService.GenerateToken(user);

        var refreshToken = new RefreshToken
        {
            UserId = user.Id,
            Token = Guid.NewGuid().ToString(),
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        };

        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            accessToken,
            refreshToken = refreshToken.Token,
            role = user.Role
        });
    }

    /* ⭐ REFRESH TOKEN */
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken(string refreshToken)
    {
        var storedToken = await _context.RefreshTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Token == refreshToken);

        if (storedToken == null || storedToken.ExpiresAt < DateTime.UtcNow)
            return Unauthorized("Invalid refresh token");

        var newAccessToken = _jwtService.GenerateToken(storedToken.User);

        return Ok(new
        {
            accessToken = newAccessToken
        });
    }

    /* ⭐ PROTECTED */
    [Authorize]
    [HttpGet("profile")]
    public IActionResult Profile()
    {
        return Ok("Authenticated User Access");
    }
}