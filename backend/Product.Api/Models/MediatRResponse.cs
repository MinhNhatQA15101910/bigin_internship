namespace Product.Api.Models;

public class MediatRResponse<T>
{
    public int StatusCode { get; set; }
    public T? Data { get; set; }
    public List<string> Errors { get; set; } = [];

    public static MediatRResponse<T> Success(T data, int statusCode = 200) => new() { StatusCode = statusCode, Data = data };
    public static MediatRResponse<T> Failure(int statusCode, params string[] errors) => new() { StatusCode = statusCode, Errors = [.. errors] };
}
