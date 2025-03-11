namespace Product.Api.Dtos;

public class ProductDto
{
    public string? Id { get; set; }
    public required string ProductName { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
