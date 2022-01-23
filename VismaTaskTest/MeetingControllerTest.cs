using System.Linq;
using System.Reflection;
using VismaTask.Controllers;
using VismaTask.Models.Meetings;
using VismaTaskTest.Stubs;
using Xunit;

namespace VismaTaskTest;

public class MeetingControllerTest
{
    [Fact]
    public void GetAll_NoFilterSet_ReturnsAllElements()
    {
        var repositoryStub = new MeetingRepositoryStub();
        var controller = new MeetingController(repositoryStub);
        var filter = new MeetingFilter();
        var listFormRepository = repositoryStub.GetAll();
        
        var actual = controller.GetAll(filter);

        Assert.Equal(listFormRepository.Count, actual.Count);
    }
    
    [Fact]
    public void GetAll_FilterSet_ReturnsZeroElements()
    {
        var repositoryStub = new MeetingRepositoryStub();
        var controller = new MeetingController(repositoryStub);
        var filter = new MeetingFilter(){NumberOfAttendees = 1};

        var actual = controller.GetAll(filter);

        Assert.Empty(actual);
    }
    
    [Fact]
    public void GetAll_FilterSet_ReturnsFilteredElements()
    {
        var repositoryStub = new MeetingRepositoryStub();
        var controller = new MeetingController(repositoryStub);
        var filter = new MeetingFilter(){Category = MeetingCategory.Short};
        var listFormRepository = repositoryStub.GetAll().
            Where(m => m.Category == MeetingCategory.Short).
            ToList();

        var actual = controller.GetAll(filter);
        Assert.Equal(listFormRepository.Count, actual.Count);
    }
}