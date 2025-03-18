namespace SharedKernel.Params;

public class ProductParams : PaginationParams
{
    public string? ProductName { get; set; }
    public decimal MinPrice { get; set; } = 0;
    public decimal MaxPrice { get; set; } = decimal.MaxValue;
    public string SortBy { get; set; } = "updatedAt";
    public string OrderBy { get; set; } = "desc";
}
