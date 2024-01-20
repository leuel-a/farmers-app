using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BackendFarmersApp.WebApi.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("first_name")]
    public string? FirstName { get; set; }
    
    [BsonElement("last_name")]
    public string? LastName { get; set; }
    
    [BsonElement("email_address")]
    public string? EmailAddress { get; set; }
    
    [BsonElement("password")]
    public string? Password { get; set; }
    
    [BsonElement("created_at")]
    public DateTimeOffset CreatedAt { get; set; }
    
    [BsonElement("updated_at")]
    public DateTimeOffset UpdatedAt { get; set; }

    public User()
    {
        CreatedAt = DateTimeOffset.UtcNow;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}