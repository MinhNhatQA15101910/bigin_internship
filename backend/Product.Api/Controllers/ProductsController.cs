using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Product.Api.Dtos;
using Product.Api.Interfaces;

namespace Product.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(
    IProductRepository productRepository,
    IMapper mapper
) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(string id)
    {
        var product = await productRepository.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        return Ok(mapper.Map<ProductDto>(product));
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> AddProduct(AddProductDto addProductDto)
    {
        var product = mapper.Map<Models.Product>(addProductDto);
        await productRepository.AddProductAsync(product);

        return CreatedAtAction(
            nameof(GetProduct),
            new { id = product.Id },
            mapper.Map<ProductDto>(product)
        );
    }
}
