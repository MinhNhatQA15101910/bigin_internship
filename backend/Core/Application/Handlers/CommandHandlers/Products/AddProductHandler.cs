using Application.Commands.Products;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories.MongoDb;
using SharedKernel.DTOs;

namespace Application.Handlers.CommandHandlers.Products;

public class AddProductCommandHandler(
    IProductRepository productRepository,
    MongoDbTransactionManager transactionManager,
    IMapper mapper
) : ICommandHandler<AddProductCommand, ProductDto>
{
    public async Task<ProductDto> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var existingProduct = await productRepository.GetProductByNameAsync(request.AddProductDto.ProductName);
        if (existingProduct != null)
        {
            throw new BadRequestException("Product with this name already exists");
        }

        var product = mapper.Map<Product>(request.AddProductDto);

        await transactionManager.ExecuteTransactionAsync(async session =>
        {
            await productRepository.AddProductAsync(product, session, cancellationToken);
        });

        return mapper.Map<ProductDto>(product);
    }
}
