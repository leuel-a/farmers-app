using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FarmersApi.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    
    [BsonElement("firstname")]
    public string? FirstName { get; set; }
    
    [BsonElement("lastname")]
    public string? LastName { get; set; }
    
    [BsonElement("email_address")]
    public string? EmailAddress { get; set; }
    
    [BsonElement("password")]
    public string? Password { get; set; }
    
    [BsonElement("created_at")]
    public DateTimeOffset CreatedAt { get; set; }
    
    [BsonElement("updated_at")]
    public DateTimeOffset UpdatedAt { get; set; }
}