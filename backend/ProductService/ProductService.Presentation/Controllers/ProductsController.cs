using MediatR;
using Microsoft.AspNetCore.Mvc;
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
}
