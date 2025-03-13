namespace Product.Api.Exceptions;

public abstract class NotFoundException(string message) : ApplicationException("Not Found", message)
{
}
