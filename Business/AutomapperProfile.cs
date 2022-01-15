using AutoMapper;
using Business.Records;
using Data.Entities;

namespace Business;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<Product, ProductRecord>();
        CreateMap<ProductRecord, Product>()
            .ForMember(product => product.ModifiedAt, 
                options => options.MapFrom(productRecord => DateTime.Now));
        
        CreateMap<Category, CategoryRecord>().ReverseMap();
    }
}