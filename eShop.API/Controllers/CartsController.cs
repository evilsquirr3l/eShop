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
    public class CartsController : ControllerBase
    {
        private readonly ICrudInterface<CartDto> _cartService;

        public CartsController(ICrudInterface<CartDto> cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartDto>>> GetAll() 
            => Ok(await _cartService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<CartDto>> GetById(int id)
            => Ok(await _cartService.GetByIdAsync(id));
        
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] CartDto cartDto)
        {
            await _cartService.AddAsync(cartDto);
        
            return CreatedAtAction(nameof(Add), new {cartDto.Id}, cartDto);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, CartDto cartDto)
        {
            await _cartService.UpdateAsync(id, cartDto);
            
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _cartService.DeleteByIdAsync(id);
        
            return Ok();
        }
    }
}