using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstraction;
using Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace eShop.API.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetById(int id)
        {
            var book = await _categoryService.GetByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CategoryDto categoryDto)
        {
            try
            {
                await _categoryService.CreateAsync(categoryDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return CreatedAtAction(nameof(Create), new {categoryDto.Id}, categoryDto);
        }

        [HttpPut]
        public async Task<ActionResult> Update(CategoryDto categoryDto)
        {
            try
            {
                await _categoryService.UpdateAsync(categoryDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(CategoryDto categoryDto)
        {
            await _categoryService.DeleteAsync(categoryDto);

            return Ok();
        }
    }
}