using System.Diagnostics.CodeAnalysis;
using Business.Interfaces;
using Business.Paging;
using Business.Records;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApi.Controllers;

[SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalse")]


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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductRecord>> GetProduct(int id)
    {
        var product = await _productService.GetProductAsync(id);

        return product is not null ? product : NotFound();
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedList<ProductRecord>>> GetProducts([FromQuery] QueryStringParameters queryStringParameters)
    {
        var products = await _productService.GetProductsListAsync(queryStringParameters);
        
        var metadata = new
        {
            products.TotalCount,
            products.PageSize,
            products.CurrentPage,
            products.TotalPages,
            products.HasNext,
            products.HasPrevious
        };
        
        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
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