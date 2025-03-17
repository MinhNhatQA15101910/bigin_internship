using AutoMapper;
using Product.Api.Dtos;
using Product.Api.Exceptions;
using Product.Api.Features.Commands;
using Product.Api.Interfaces;

namespace Product.Api.Features.Handlers.CommandHandlers;

public class AddProductCommandHandler(
    IProductRepository productRepository,
    IMapper mapper
) : ICommandHandler<AddProductCommand, ProductDto>
{
    public async Task<ProductDto> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var existingProduct = await productRepository.GetProductByNameAsync(request.ProductName);
        if (existingProduct != null)
        {
            throw new BadRequestException("Product with this name already exists");
        }

        var product = mapper.Map<Models.Product>(request);

        await productRepository.AddProductAsync(product, cancellationToken);

        return mapper.Map<ProductDto>(product);
    }
}
