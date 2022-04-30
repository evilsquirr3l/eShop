using System.Diagnostics.CodeAnalysis;
using Business.Interfaces;
using Business.Records;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Models;

namespace WebApi.Controllers;

[SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalse")]


[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly int _defaultSkipLimit;
    private readonly int _defaultTakeLimit;

    public ProductsController(IProductService productService, IOptions<AppConfiguration> configuration)
    {
        _productService = productService;
        
        _defaultSkipLimit = configuration.Value.DefaultSkipLimit;
        _defaultTakeLimit = configuration.Value.DefaultTakeLimit;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductRecord>> GetProduct(int id)
    {
        var product = await _productService.GetProductAsync(id);

        return product is not null ? product : NotFound();
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ResultSet<ProductRecord>>> GetProducts([FromQuery] int? skip = null, [FromQuery] int? take = null)
    {
        var paginationModel = new PaginationModel()
        {
            Skip = skip ?? _defaultSkipLimit,
            Take = take ?? _defaultTakeLimit
        };
        
        var products = await _productService.GetProductsListAsync(paginationModel);
        
        return products;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateProduct([FromBody] ProductRecord productRecord)
    {
        await _productService.CreateProductAsync(productRecord);
        
        return CreatedAtAction(nameof(GetProduct), new {id = productRecord.Id}, productRecord);
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateProduct(int id, ProductRecord productRecord)
    {
        if (id != productRecord.Id)
        {
            return BadRequest();
        }
        
        var product = await _productService.GetProductAsync(id);

        if (product is null)
        {
            return NotFound();
        }

        await _productService.UpdateProductAsync(id, productRecord);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await _productService.GetProductAsync(id);

        if (product is null)
        {
            return NotFound();
        }
        
        await _productService.DeleteProductAsync(id);

        return NoContent();
    }
}