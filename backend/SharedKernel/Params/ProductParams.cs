namespace SharedKernel.Params;

public class ProductParams : PaginationParams
{
    public string? ProductName { get; set; }
    public string? Category { get; set; }
    public decimal MinPrice { get; set; } = decimal.MinValue;
    public decimal MaxPrice { get; set; } = decimal.MaxValue;
    public string OrderBy { get; set; } = "updatedAt";
    public string SortBy { get; set; } = "desc";
}
