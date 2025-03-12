using AutoMapper;
using MediatR;
using Product.Api.Dtos;
using Product.Api.Interfaces;
using Product.Api.Models;

namespace Product.Api.Features.Commands;

public class AddProductCommandHandler(
    IMapper mapper,
    IProductRepository productRepository
) : IRequestHandler<AddProductCommand, MediatRResponse<ProductDto>>
{
    public async Task<MediatRResponse<ProductDto>> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var errors = new List<string>();

        var existingProduct = await productRepository.GetProductByNameAsync(request.ProductName);
        if (existingProduct != null)
        {
            errors.Add("Product with this name already exists");
        }

        if (errors.Count != 0)
        {
            return MediatRResponse<ProductDto>.Failure(400, [.. errors]);
        }

        var product = mapper.Map<Models.Product>(request);

        await productRepository.AddProductAsync(product, cancellationToken);

        return MediatRResponse<ProductDto>.Success(mapper.Map<ProductDto>(product));
    }
}
