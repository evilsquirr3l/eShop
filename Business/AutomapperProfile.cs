using AutoMapper;
using Business.Records;
using Database.Entities;

namespace Business;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<Product, ProductRecord>().ReverseMap();
        CreateMap<Category, CategoryRecord>().ReverseMap();
    }
}