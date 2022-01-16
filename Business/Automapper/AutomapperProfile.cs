using AutoMapper;
using Business.Records;
using Data.Entities;

namespace Business.Automapper;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<Product, ProductRecord>();
        CreateMap<ProductRecord, Product>()
            .ForMember(product => product.ModifiedAt, 
                options => options.MapFrom<ModifiedAtResolver>());
        
        CreateMap<Category, CategoryRecord>().ReverseMap();
    }
}