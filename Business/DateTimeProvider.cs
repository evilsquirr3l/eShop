using System.Diagnostics.CodeAnalysis;
using Business.Interfaces;

namespace Business;

[ExcludeFromCodeCoverage]
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetCurrentTime() => DateTime.Now;
}