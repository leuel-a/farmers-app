using BackendFarmersApp.WebApi.Models;
using BackendFarmersApp.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendFarmersApp.WebApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UsersServices _usersServices;

    public UsersController(UsersServices usersServices)
    {
        _usersServices = usersServices;
    }

    [HttpGet]
    public async Task<List<User>> GetUsersAsync()
    {
        return await _usersServices.GetUsersAsync();
    }

    [HttpPost]
    public async Task<IActionResult> CreateUserAsync(User user)
    {
        if (user.FirstName is null) return new BadRequestObjectResult(new { error = "First name is required." });
        if (user.LastName is null) return new BadRequestObjectResult(new { error = "Last name is required." });
        if (user.EmailAddress is null) return new BadRequestObjectResult(new { error = "Email address is required." });

        var existingUser = await _usersServices.GetUserByEmailAsync(user.EmailAddress);
        if (existingUser is not null)
            return new BadRequestObjectResult(new { error = "Email address already exists." });

        if (user.Password is null) return new BadRequestObjectResult(new { error = "Password is required." });
        await _usersServices.CreateUserAsync(user);
        return new OkObjectResult(user);
    }
}