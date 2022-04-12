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
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryRecord>> GetCategory(int id)
    {
        var category = await _categoryService.GetCategoryAsync(id);

        return category is not null ? category : NotFound();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedList<CategoryRecord>>> GetCategories([FromQuery] QueryStringParameters queryStringParameters)
    {
        var categories = await _categoryService.GetCategoryListAsync(queryStringParameters);
        
        var metadata = new
        {
            categories.TotalCount,
            categories.PageSize,
            categories.CurrentPage,
            categories.TotalPages,
            categories.HasNext,
            categories.HasPrevious
        };
        
        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
        return categories;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateCategory([FromBody] CategoryRecord categoryRecord)
    {
        await _categoryService.CreateCategoryAsync(categoryRecord);
        
        return CreatedAtAction(nameof(GetCategory), new {id = categoryRecord.Id}, categoryRecord);
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateCategory(int id, CategoryRecord categoryRecord)
    {
        if (id != categoryRecord.Id)
        {
            return BadRequest();
        }
        
        var category = await _categoryService.GetCategoryAsync(id);

        if (category is null)
        {
            return NotFound();
        }

        await _categoryService.UpdateCategoryAsync(id, categoryRecord);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteCategory(int id)
    {
        var category = await _categoryService.GetCategoryAsync(id);

        if (category is null)
        {
            return NotFound();
        }
        
        await _categoryService.DeleteCategoryAsync(id);

        return NoContent();
    }
}