namespace Auth.Api.Helpers;

public class UserParams : PaginationParams
{
    public string? CurrentEmail { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? OrderBy { get; set; } = "email";
    public string? SortBy { get; set; } = "asc";
}
