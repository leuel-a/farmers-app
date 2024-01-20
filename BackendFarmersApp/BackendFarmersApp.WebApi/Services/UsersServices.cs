using MongoDB.Driver;
using BackendFarmersApp.WebApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BackendFarmersApp.WebApi.Services;

public class UsersServices
{
    private readonly IMongoCollection<User> _usersCollection;

    public UsersServices(IOptions<FarmersAppDatabaseSettings> farmersAppDatabaseSettings)
    {
        var client = new MongoClient(farmersAppDatabaseSettings.Value.ConnectionString);
        var database = client.GetDatabase(farmersAppDatabaseSettings.Value.DatabaseName);
        _usersCollection = database.GetCollection<User>(farmersAppDatabaseSettings.Value.UsersCollectionName);
    }

    public async Task<List<User>> GetUsersAsync() =>
        await _usersCollection.Find(user => true).ToListAsync();
    
    public async Task<User?> GetUserByIdAsync(string id) =>
        await _usersCollection.Find(user => user.Id == id).FirstOrDefaultAsync();
    
    public async Task<User?> GetUserByEmailAsync(string emailAddress) =>
        await _usersCollection.Find(user => user.EmailAddress == emailAddress).FirstOrDefaultAsync();

    public async Task CreateUserAsync(User user) =>
        await _usersCollection.InsertOneAsync(user);
}