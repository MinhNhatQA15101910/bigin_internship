using AutoMapper;
using ProductService.Core.Application.Commands;
using ProductService.Core.Domain.Entities;
using ProductService.Core.Domain.Repositories;
using SharedKernel.Exceptions;

namespace ProductService.Core.Application.Handlers.CommandHandlers;

public class UpdateProductHandler(
    IProductRepository productRepository,
    IMapper mapper
) : ICommandHandler<UpdateProductCommand, bool>
{
    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        _ = await productRepository.GetProductByIdAsync(request.Id, cancellationToken)
            ?? throw new ProductNotFoundException(request.Id);

        if (request.UpdateProductDto.ProductName != null)
        {
            var existingProduct = await productRepository.GetProductByNameAsync(request.UpdateProductDto.ProductName, cancellationToken);
            if (existingProduct != null && existingProduct.Id != request.Id)
            {
                throw new BadRequestException("Product name already exists");
            }
        }

        var product = mapper.Map<Product>(request.UpdateProductDto);
        product.Id = request.Id;

        var result = await productRepository.UpdateProductAsync(request.Id, product, cancellationToken);
        if (result.IsAcknowledged && result.ModifiedCount > 0)
        {
            return true;
        }

        throw new BadRequestException("Failed to update product");
    }
}
