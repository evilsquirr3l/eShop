using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.Models;
using Data.Entities;
using Data.Implementation;
using Microsoft.EntityFrameworkCore;

namespace Business.Implementation
{
    class CartService
    {
        private readonly IMapper _mapper;
        private readonly ShopDbContext _dbContext;

        public CartService(IMapper mapper, ShopDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task Create(CartDto cartDto)
        {
           await _dbContext.Carts.AddAsync((_mapper.Map<Cart>(cartDto)));
           await _dbContext.SaveChangesAsync();
        }
        public async Task<CartDto> GetById(int id)
        {
            return _mapper.Map<CartDto>(await _dbContext.Carts.FindAsync(id));
        }

        public async Task<IEnumerable<CartDto>> GetAll()
        {
            var carts = await _dbContext.Carts.ToListAsync();
            return _mapper.Map<IEnumerable<CartDto>>(carts);
        }

        public async Task Update(CartDto cartDto)
        {
             _dbContext.Carts.Update(_mapper.Map<Cart>(cartDto));
           await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(CartDto cartDto)
        {
            _dbContext.Carts.Remove(_mapper.Map<Cart>(cartDto));
           await _dbContext.SaveChangesAsync();
        }
    }
}

