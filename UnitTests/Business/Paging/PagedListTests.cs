using System.Collections.Generic;
using Business.Paging;
using FluentAssertions;
using NUnit.Framework;

namespace UnitTests.Business.Paging;

[TestFixture]
public class PagedListTests
{
    [Test]
    public void PagedList_HasOnePage_HasNextAndHasPreviousAreCorrect()
    {
        var items = new List<string> {"1", "2"};
        var count = items.Count;
        var pageNumber = 1;
        var pageSize = 2;
        
        var pagedList = PagedList<string>.ToPagedList(items, count, pageNumber, pageSize);

        pagedList.HasNext.Should().BeFalse();
        pagedList.HasPrevious.Should().BeFalse();
    }
    
    [Test]
    public void PagedList_HasThreePagesAndCurrentPageIsSecond_HasNextAndHasPreviousAreCorrect()
    {
        var items = new List<string> {"1", "2", "3"};
        var count = items.Count;
        var pageNumber = 2;
        var pageSize = 1;
        
        var pagedList = PagedList<string>.ToPagedList(items, count, pageNumber, pageSize);

        pagedList.HasNext.Should().BeTrue();
        pagedList.HasPrevious.Should().BeTrue();
    }
}