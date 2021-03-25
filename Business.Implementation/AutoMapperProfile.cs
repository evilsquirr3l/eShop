using AutoMapper;
using Business.Models;
using Data.Entities;

namespace Business.Implementation
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductDto, Product>().ReverseMap();

            CreateMap<CartDto, Cart>().ReverseMap();

            CreateMap<CategoryDto, Category>().ReverseMap();

            CreateMap<CustomerDto, Customer>().ReverseMap();
        }
    }
}