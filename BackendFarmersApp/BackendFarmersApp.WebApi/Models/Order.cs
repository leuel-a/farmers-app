using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BackendFarmersApp.WebApi.Models;

public class Order
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("consumer_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? ConsumerId { get; set; }
    
    [BsonElement("order_date")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTimeOffset OrderDate { get; set; }
    
    [BsonElement("status")]
    public string Status { get; set; } // Pending, Complete
    
    public List<OrderDetail> OrderDetails { get; set; }
}