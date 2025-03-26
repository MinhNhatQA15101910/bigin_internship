namespace SharedKernel.Exceptions;

public class ProductNotFoundException(string productId)
    : NotFoundException($"The product with the identifier {productId} was not found.")
{
}
