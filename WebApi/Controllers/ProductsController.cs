using Business.Interfaces;
using Business.Records;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<ProductRecord>> GetProduct(int id)
    {
        var product = await _productService.GetProductAsync(id);

        return product is not null ? product : NotFound();
    }
}