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
    
    [BsonElement("role")]
    public string? Role { get; set; }
    
    [BsonElement("password")]
    public string? PasswordHash { get; set; }
    
    [BsonElement("created_at")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime CreatedAt { get; set; }
    
    [BsonElement("updated_at")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime UpdatedAt { get; set; }
}