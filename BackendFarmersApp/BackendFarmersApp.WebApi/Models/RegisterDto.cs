namespace BackendFarmersApp.WebApi.Models;

public class RegisterDto
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Password { get; init; }
    public string? EmailAddress { get; init; }
    public UserRole? Role { get; init; }
}