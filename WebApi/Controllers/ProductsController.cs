using System.Diagnostics.CodeAnalysis;
using Business.Interfaces;
using Business.Records;
using Microsoft.AspNetCore.Mvc;

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
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateProduct([FromBody] ProductRecord productRecord)
    {
        await _productService.CreateProductAsync(productRecord);
        
        return CreatedAtAction(nameof(GetProduct), new {id = productRecord.Id}, productRecord);
    }
    
    [HttpPut]
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
    public async Task<ActionResult> DeleteProduct(int productId)
    {
        var product = await _productService.GetProductAsync(productId);

        if (product is null)
        {
            return NotFound();
        }
        
        await _productService.DeleteProductAsync(productId);

        return NoContent();
    }
}