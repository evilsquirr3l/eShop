using AutoMapper;
using Business.Interfaces;
using Business.Records;
using Data.Entities;

namespace Business.Automapper;

public class ModifiedAtResolver : IValueResolver<ProductRecord, Product, DateTime>
{
    private readonly IDateTimeProvider _dateTimeProvider;

    
    public ModifiedAtResolver(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public DateTime Resolve(ProductRecord source, Product destination, DateTime member, ResolutionContext context)
    {
        return _dateTimeProvider.GetCurrentTime();
    }
}