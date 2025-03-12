using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        var result = await mediator.Send(new GetProductByIdQuery(id));

        return result.StatusCode switch
        {
            200 => Ok(result.Data),
            404 => NotFound(result.Errors),
            _ => Problem(result.Errors.First())
        };
    }

    [HttpPost]
    // [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ProductDto>> AddProduct(AddProductCommand addProductCommand)
    {
        var result = await mediator.Send(addProductCommand);

        return result.StatusCode switch
        {
            200 => CreatedAtAction(
                nameof(GetProduct),
                new { id = result.Data!.Id },
                result.Data
            ),
            400 => BadRequest(result.Errors),
            _ => Problem(result.Errors.First())
        };
    }
}
