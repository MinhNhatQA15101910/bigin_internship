using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductService.Core.Application.Commands;
using ProductService.Core.Application.DTOs;
using ProductService.Core.Application.Queries;
using SharedKernel.DTOs;

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

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ProductDto>> CreateProduct(AddUpdateProductDto addProductDto)
    {
        var product = await mediator.Send(new AddProductCommand(addProductDto));
        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
    }
}
