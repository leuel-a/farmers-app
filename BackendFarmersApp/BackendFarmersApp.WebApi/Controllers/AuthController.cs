using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BackendFarmersApp.WebApi.Models;
using BackendFarmersApp.WebApi.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BackendFarmersApp.WebApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UsersServices _usersServices;
    private readonly IConfiguration _configuration;
    private string _tokenSecret;

    private static User user = new User();

    public AuthController(UsersServices usersServices, IConfiguration configuration)
    {
        _usersServices = usersServices;
        _configuration = configuration;
        _tokenSecret = _configuration.GetSection("JwtSettings:Key").Value!;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        var validationError = ValidateDto(registerDto);
        if (validationError.Length > 0)
        {
            return BadRequest(validationError);
        }

        var emailExists = await CheckEmailExists(registerDto.EmailAddress!);
        if (emailExists)
        {
            return BadRequest("email already exists");
        }

        user.FirstName = registerDto.FirstName;
        user.LastName = registerDto.LastName;
        user.EmailAddress = registerDto.EmailAddress;
        user.Role = registerDto.Role.ToString();
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

        await _usersServices.CreateUserAsync(user);
        return Ok(new
            { message = "user created successfully", user = new { email = user.EmailAddress, role = user.Role } });
    }

    private async Task<bool> CheckEmailExists(string emailAddress)
    {
        var user = await _usersServices.GetUserByEmailAsync(emailAddress);
        return user != null;
    }

    private static string ValidateDto(RegisterDto registerDto)
    {
        var errorMessages = new Dictionary<string, string>
        {
            { nameof(RegisterDto.FirstName), "first name is required" },
            { nameof(RegisterDto.LastName), "last name is required" },
            { nameof(RegisterDto.EmailAddress), "email address is required" },
            { nameof(RegisterDto.Password), "password is required" },
            { nameof(RegisterDto.Role), "role is required" },
        };

        foreach (var property in typeof(RegisterDto).GetProperties())
        {
            if (property.GetValue(registerDto) == null && errorMessages.TryGetValue(property.Name, out var value))
            {
                return value;
            }
        }

        return string.Empty;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        if (loginDto.EmailAddress is null)
        {
            return BadRequest("email address is required");
        }

        if (loginDto.Password is null)
        {
            return BadRequest("password is required");
        }

        var userFromDb = await _usersServices.GetUserByEmailAsync(loginDto.EmailAddress);
        if (userFromDb == null)
        {
            return BadRequest("invalid email address or password");
        }

        if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, userFromDb.PasswordHash))
        {
            return BadRequest("invalid email address or password");
        }

        return Ok(userFromDb);
    }
    
}