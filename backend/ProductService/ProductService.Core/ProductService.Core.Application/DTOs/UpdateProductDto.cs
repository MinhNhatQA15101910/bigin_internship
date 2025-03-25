namespace ProductService.Core.Application.DTOs;

public class UpdateProductDto
{
    public string? ProductName { get; set; }
    public string? Category { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public int? StockQuantity { get; set; }
}
