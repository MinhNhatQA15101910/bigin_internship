using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductService.Core.Application.Commands;
using ProductService.Core.Application.DTOs;
using ProductService.Core.Application.Queries;
using ProductService.Presentation.Extensions;
using SharedKernel.DTOs;
using SharedKernel.Params;

namespace ProductService.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<ProductDto>> GetProductById(string id)
    {
        var product = await mediator.Send(new GetProductByIdQuery(id));
        return Ok(product);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers([FromQuery] ProductParams productParams)
    {
        var products = await mediator.Send(new GetProductsQuery(productParams));

        Response.AddPaginationHeader(products);

        return Ok(products);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ProductDto>> CreateProduct(AddProductDto addProductDto)
    {
        var product = await mediator.Send(new AddProductCommand(addProductDto));
        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
    }

    [HttpPut("{id:length(24)}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ProductDto>> UpdateProduct(string id, UpdateProductDto updateProductDto)
    {
        await mediator.Send(new UpdateProductCommand(id, updateProductDto));
        return NoContent();
    }
}
