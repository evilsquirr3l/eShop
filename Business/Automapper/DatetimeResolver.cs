using AutoMapper;
using Business.Interfaces;
using Business.Records;
using Data.Entities;

namespace Business.Automapper;

public class DatetimeResolver : IValueResolver<ProductRecord, Product, DateTime>
{
    private readonly IDateTimeProvider _dateTimeProvider;
    
    public DatetimeResolver(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public DateTime Resolve(ProductRecord source, Product destination, DateTime member, ResolutionContext context)
    {
        return _dateTimeProvider.GetCurrentTime();
    }
}