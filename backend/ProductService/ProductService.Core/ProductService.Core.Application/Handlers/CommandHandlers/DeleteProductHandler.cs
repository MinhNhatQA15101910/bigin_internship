using ProductService.Core.Application.Commands;
using ProductService.Core.Domain.Repositories;
using SharedKernel.Exceptions;

namespace ProductService.Core.Application.Handlers.CommandHandlers;

public class DeleteProductHandler(IProductRepository productRepository) : ICommandHandler<DeleteProductCommand, bool>
{
    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetProductByIdAsync(request.Id, cancellationToken)
            ?? throw new ProductNotFoundException(request.Id);

        var result = await productRepository.DeleteProductAsync(product, cancellationToken);
        if (result.DeletedCount == 0)
        {
            throw new BadRequestException("Product was not deleted");
        }

        return true;
    }
}
