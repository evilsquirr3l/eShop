using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstraction;
using Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace eShop.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICrudInterface<CategoryDto> _categoryService;

        public CategoryController(ICrudInterface<CategoryDto> categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll() 
            => Ok(await _categoryService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetById(int id)
            => Ok(await _categoryService.GetByIdAsync(id));
        
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] CategoryDto categoryDto)
        {
            try
            {
                await _categoryService.AddAsync(categoryDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        
            return CreatedAtAction(nameof(Add), new {categoryDto.Id}, categoryDto);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, CategoryDto categoryDto)
        {
            try
            {
                await _categoryService.UpdateAsync(id, categoryDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _categoryService.DeleteByIdAsync(id);
        
            return Ok();
        }
    }
}