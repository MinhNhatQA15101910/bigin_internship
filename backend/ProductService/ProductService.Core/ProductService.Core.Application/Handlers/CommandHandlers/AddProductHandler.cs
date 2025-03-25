using AutoMapper;
using ProductService.Core.Application.Commands;
using ProductService.Core.Domain.Entities;
using ProductService.Core.Domain.Repositories;
using SharedKernel.DTOs;
using SharedKernel.Exceptions;

namespace ProductService.Core.Application.Handlers.CommandHandlers;

public class AddProductHandler(IProductRepository productRepository, IMapper mapper) : ICommandHandler<AddProductCommand, ProductDto>
{
    public async Task<ProductDto> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var existingProduct = await productRepository.GetProductByNameAsync(request.AddProductDto.ProductName, cancellationToken);
        if (existingProduct != null)
        {
            throw new BadRequestException("Product with this name already exists");
        }

        var product = mapper.Map<Product>(request.AddProductDto);

        await productRepository.AddProductAsync(product, cancellationToken);

        return mapper.Map<ProductDto>(product);
    }
}
