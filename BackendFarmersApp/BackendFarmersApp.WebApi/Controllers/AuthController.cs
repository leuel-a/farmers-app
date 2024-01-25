using BackendFarmersApp.WebApi.Models;
using BackendFarmersApp.WebApi.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BackendFarmersApp.WebApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UsersServices _usersServices;

    public AuthController(UsersServices usersServices)
    {
        _usersServices = usersServices;
    }

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

        var user = new User()
        {
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            EmailAddress = registerDto.EmailAddress,
            Role = registerDto.Role.ToString(),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
        };
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
}