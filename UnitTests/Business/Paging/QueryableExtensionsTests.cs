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
        var queryStringsParameter = new PaginationModel() {Skip = 1, Take = 2};
        var result = strings.Paginate(queryStringsParameter);

        result.Should().Contain("2");
        result.Should().Contain("3");
    }
}
