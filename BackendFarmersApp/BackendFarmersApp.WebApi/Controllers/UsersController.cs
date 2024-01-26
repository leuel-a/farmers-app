using BackendFarmersApp.WebApi.Models;
using BackendFarmersApp.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
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
    
    [HttpGet("all")]
    public async Task<List<User>> GetUsersAsync()
    {
        return await _usersServices.GetUsersAsync();
    }
}