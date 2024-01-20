using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using FarmersApp.WebApi.Models;
using Microsoft.AspNetCore.Identity;

namespace FarmersApp.WebApi.Services;

public class UsersService
{
    private readonly IMongoCollection<User> _usersCollection;
    private readonly PasswordHasher<User> _passwordHasher;

    public UsersService(
        IOptions<DatabaseModel> farmersDatabaseSettings
    )
    {
        var mongoClient = new MongoClient(farmersDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(farmersDatabaseSettings.Value.DatabaseName);
        _usersCollection = mongoDatabase.GetCollection<User>(farmersDatabaseSettings.Value.UsersCollectionName);


        // Create an instance of the password hasher
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task<List<User>> GetAsync() => await _usersCollection.Find(_ => true).ToListAsync();

    public async Task<IActionResult> CreateAsync(User newUser)
    {
        if (newUser.FirstName is null)
        {
            return new BadRequestObjectResult("Error: First name cannot be null");
        }

        if (newUser.LastName is null)
        {
            return new BadRequestObjectResult("Error: Last name cannot be null");
        }

        if (newUser.EmailAddress is null)
        {
            return new BadRequestObjectResult("Error: Please specify an email address.");
        }

        // Check if there is an existing user with the email address
        var emailExists = await _usersCollection.Find(user => user.EmailAddress == newUser.EmailAddress).ToListAsync();
        if (emailExists.Count > 1)
        {
            return new BadRequestObjectResult("Error: Email address already in use");
        }

        if (newUser.Password is null)
        {
            return new BadRequestObjectResult("Error: Please provide a password");
        }

        await _usersCollection.InsertOneAsync(newUser);

        return new OkObjectResult(new
        {
            newUser.EmailAddress,
            newUser.FirstName,
            newUser.LastName,
        });
    }
}