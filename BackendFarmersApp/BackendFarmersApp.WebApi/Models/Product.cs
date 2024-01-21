using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BackendFarmersApp.WebApi.Models;

public class Product
{
   [BsonId]
   [BsonRepresentation(BsonType.ObjectId)]
   public string? Id { get; set; }

   [BsonElement("name")]
   public string? Name { get; set; }

   [BsonElement("description")]
   public string? Description { get; set; }
   
   [BsonElement("price")]
   public decimal Price { get; set; }

   [BsonElement("quantity")]
   public int Quantity { get; set; }
    
   [BsonElement("category")]
   public string? Category { get; set; }
   
   [BsonElement("farmer_id")]
   public string? FarmerId { get; set; }
}