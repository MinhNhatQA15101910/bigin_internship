namespace Product.Api.Dtos;

public class AddProductDto
{
    public required string ProductName { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}
