using System;
using System.Linq;
using System.Reflection;
using VismaTask.Controllers;
using VismaTask.Models.Meetings;
using VismaTask.Services.Meetings;
using VismaTaskTest.Stubs;
using Xunit;

namespace VismaTaskTest;

public class MeetingControllerTest : IDisposable
{
    private MeetingController _controller;
    
    public MeetingControllerTest()
    {
        _controller = new MeetingController(new MeetingRepositoryStub(), new MeetingService(), "Admin");
    }

    public void Dispose()
    {
    }
    
    [Fact]
    public void GetAll_NoFilterSet_ReturnsAllElements()
    {
        var repositoryStub = new MeetingRepositoryStub();
        var filter = new MeetingFilter();
        var listFormRepository = repositoryStub.GetAll();
        
        var actual = _controller.GetAll(filter);

        Assert.Equal(listFormRepository.Count, actual.Count);
    }
    
    [Fact]
    public void GetAll_FilterSet_ReturnsZeroElements()
    {
        var repositoryStub = new MeetingRepositoryStub();
        var filter = new MeetingFilter(){NumberOfAttendees = 1};

        var actual = _controller.GetAll(filter);

        Assert.Empty(actual);
    }
    
    [Fact]
    public void GetAll_FilterSet_ReturnsFilteredElements()
    {
        var repositoryStub = new MeetingRepositoryStub();
        var filter = new MeetingFilter(){Category = MeetingCategory.Short};
        var listFormRepository = repositoryStub.GetAll().
            Where(m => m.Category == MeetingCategory.Short).
            ToList();

        var actual = _controller.GetAll(filter);
        Assert.Equal(listFormRepository.Count, actual.Count);
    }

    [Fact]
    public void Get_ReturnsCorrectElement()
    {
        int id = 1;
        var repositoryStub = new MeetingRepositoryStub();
        var meetingFromRepository = repositoryStub.Get(id);

        var actual = _controller.Get(id);
        
        Assert.Equal(meetingFromRepository.MeetingId, actual.MeetingId);
    }

}