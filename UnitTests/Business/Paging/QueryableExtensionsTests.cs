using System.Collections.Generic;
using System.Linq;
using Business.Paging;
using Business.Records;
using FluentAssertions;
using NUnit.Framework;

namespace UnitTests.Business.Paging;

[TestFixture]
public class QueryableExtensionsTests
{
    [Test]
    public void Paginate_WithListOfThreeItems_ReturnsCorrectValue()
    {
        var strings = new List<string> {"1", "2", "3"}.AsQueryable();
        var queryStringsParameter = new QueryStringParameters {CurrentPage = 2, PageSize = 1};
        var result = strings.Paginate(queryStringsParameter);

        result.Should().Contain("2");
    }
}
