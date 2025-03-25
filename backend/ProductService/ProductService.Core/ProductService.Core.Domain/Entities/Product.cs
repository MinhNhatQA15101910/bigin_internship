using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProductService.Core.Domain.Entities;

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    [BsonElement("Name")]
    public string? ProductName { get; set; } = string.Empty;

    public string? Category { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public decimal? Price { get; set; } = 0;
    public int? StockQuantity { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
