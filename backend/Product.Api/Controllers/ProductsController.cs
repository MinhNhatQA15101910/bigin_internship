using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Api.Dtos;
using Product.Api.Features.Commands;
using Product.Api.Features.Queries;

namespace Product.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(string id)
    {
        var productDto = await mediator.Send(new GetProductByIdQuery(id));

        return productDto;
    }

    [HttpPost]
    // [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ProductDto>> AddProduct(AddUpdateProductDto addUpdateProductDto)
    {
        var product = await mediator.Send(new AddProductCommand(addUpdateProductDto));

        return CreatedAtAction(
            nameof(GetProduct),
            new { id = product.Id },
            product
        );
    }
}
