using Application.Commands.Products;
using Application.DTOs.Products;
using Application.Queries.Products;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extensions;
using SharedKernel.DTOs;
using SharedKernel.Params;

namespace Presentation.Controllers;

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
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ProductDto>> AddProduct(AddUpdateProductDto addUpdateProductDto)
    {
        var product = await mediator.Send(new AddProductCommand(addUpdateProductDto));

        return CreatedAtAction(
            nameof(GetProduct),
            new { id = product.Id },
            product
        );
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts([FromQuery] ProductParams productParams)
    {
        var products = await mediator.Send(new GetProductsQuery(productParams));

        Response.AddPaginationHeader(products);

        return Ok(products);
    }
}
