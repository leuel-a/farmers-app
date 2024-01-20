using FarmersApp.WebApi.Models;
using FarmersApp.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FarmersApp.WebApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UsersService _usersService;

    public UsersController(UsersService usersService) => _usersService = usersService;

    [HttpGet]
    public async Task<List<User>> Get() => await _usersService.GetAsync();
}