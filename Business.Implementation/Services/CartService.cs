using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstraction;
using Business.Models;
using Data.Entities;
using Data.Implementation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Business.Implementation.Services
{
    public class CartService : ICrudInterface<CartDto>
    {
        private readonly ShopDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly AbstractValidator<CartDto> _validator;
        private readonly IServiceHelper<Cart> _helper;

        public CartService(IMapper mapper, ShopDbContext dbContext, AbstractValidator<CartDto> validator, IServiceHelper<Cart> helper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _validator = validator;
            _helper = helper;
        }

        public async Task AddAsync(CartDto model)
        {
            await _validator.ValidateAsync(model);
            await _dbContext.Carts.AddAsync(_mapper.Map<Cart>(model));

            await _dbContext.SaveChangesAsync();
        }

        public async Task<CartDto> GetByIdAsync(int id)
        {
            return _mapper.Map<CartDto>(await _dbContext.Carts.FindAsync(id));
        }

        public async Task<IEnumerable<CartDto>> GetAllAsync()
        {
            var carts = await _dbContext.Carts.ToListAsync();

            return _mapper.Map<IEnumerable<CartDto>>(carts);
        }

        public async Task UpdateAsync(int id, CartDto model)
        {
            _helper.ThrowValidationExceptionIfModelIsNull(await _dbContext.Carts.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id));

            await _validator.ValidateAsync(model);
            _dbContext.Carts.Update(_mapper.Map<Cart>(model));

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var cart = await _dbContext.Carts.FindAsync(id);
            _helper.ThrowValidationExceptionIfModelIsNull(cart);

            _dbContext.Carts.Remove(cart);
            await _dbContext.SaveChangesAsync();
        }
    }
}