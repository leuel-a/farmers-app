using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BackendFarmersApp.WebApi.Models;

public class OrderDetail
{
    [BsonElement("product_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? ProductId { get; set; }

    [BsonElement("quantity")] public int Quantity { get; set; }

    [BsonElement("price")] public decimal Price { get; set; }
}