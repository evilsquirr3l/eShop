using AutoMapper;
using Business.Records;
using Data.Entities;

namespace Business.Automapper;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<Product, ProductRecord>().ReverseMap();
        
        CreateMap<Category, CategoryRecord>().ReverseMap();
        
        CreateMap<User, UserRecord>().ReverseMap();
    }
}