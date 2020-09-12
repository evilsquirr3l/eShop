using System;
using System.Collections.Generic;
using System.Text;
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

        public async Task Create(CartDTO cartDto)
        {
            _dbContext.Carts.AddAsync((_mapper.Map<Cart>(cartDto)));
            _dbContext.SaveChangesAsync();
        }
        public async Task<CartDTO> GetById(int id)
        {
            return _mapper.Map<CartDTO>(_dbContext.Carts.FindAsync(id));
        }

        public async Task<IEnumerable<CartDTO>> GetAll()
        {
            var carts = await _dbContext.Carts.ToListAsync();
            return _mapper.Map<IEnumerable<CartDTO>>(carts);
        }

        public async Task Update(CartDTO cartDto)
        {
            _dbContext.Carts.Update(_mapper.Map<Cart>(cartDto));
            _dbContext.SaveChangesAsync();
        }

        public async Task Delete(CartDTO cartDto)
        {
            _dbContext.Carts.Remove(_mapper.Map<Cart>(cartDto));
            _dbContext.SaveChanges();
        }
    }
}

