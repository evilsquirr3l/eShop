using AutoMapper;
using Business.Models;
using Data.Entities;

namespace Business.Implementation
{
    class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductDTO, Product>().ReverseMap();
            CreateMap<CartDTO, Cart>().ReverseMap();
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<CustomerDTO, Customer>().ReverseMap();
        }
    }
}