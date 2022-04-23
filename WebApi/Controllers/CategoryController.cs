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
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly int _defaultSkipLimit;
    private readonly int _defaultTakeLimit;

    public CategoriesController(ICategoryService categoryService, IOptions<AppConfiguration> configuration)
    {
        _categoryService = categoryService;
        _defaultSkipLimit = configuration.Value.DefaultSkipLimit;
        _defaultTakeLimit = configuration.Value.DefaultTakeLimit;
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
    public async Task<ActionResult<ResultSet<CategoryRecord>>> GetCategories([FromQuery] int? skip = null, [FromQuery] int? take = null)
    {
        var paginationModel = new PaginationModel()
        {
            Skip = skip ?? _defaultSkipLimit,
            Take = take ?? _defaultTakeLimit
        };
        
        var categories = await _categoryService.GetCategoryListAsync(paginationModel);
        
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