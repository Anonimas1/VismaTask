using System;
using System.Collections.Generic;
using VismaTask.Models.Meetings;
using Xunit;

namespace VismaTaskTest;

public class MeetingFilterTest
{
    private string _meetingName = "MeetName";
    private string _responsiblePerson = "Responsible person";
    private string _description = "Word1 Word2 word3";
    private MeetingCategory _category = MeetingCategory.Hub;
    private MeetingType _type = MeetingType.Live;
    private DateTime _startDate = new DateTime(2000, 01, 01);
    private DateTime _endDate = new DateTime(2000, 01, 02);
    private List<string> _attendees = new List<string>() { "one", "two", "three" };

    [Fact]
    public void IsValidFor_ResponsablePersonSet_ReturnsFalse()
    {
        var meetingFilter = new MeetingFilter() { ResponsiblePerson = "BAD RESPONSABLE NAME" };
        var meeting = FakeMeeting();

        var actual = meetingFilter.IsValidFor(meeting);

        Assert.False(actual);
    }

    [Fact]
    public void IsValidFor_ResponsablePersonSet_ReturnsTrue()
    {
        var meetingFilter = new MeetingFilter() { ResponsiblePerson = _responsiblePerson };
        var meeting = FakeMeeting();

        var actual = meetingFilter.IsValidFor(meeting);

        Assert.True(actual);
    }

    [Fact]
    public void IsValidFor_NoConditionsSet_ReturnsTrue()
    {
        var meetingFilter = new MeetingFilter();
        var meeting = FakeMeeting();

        var actual = meetingFilter.IsValidFor(meeting);

        Assert.True(actual);
    }

    [Fact]
    public void IsValidFor_DateInRange_ReturnsTrue()
    {
        var meetingFilter = new MeetingFilter() { StartDate = _startDate.AddHours(-1), EndDate = _endDate.AddHours(1) };
        var meeting = FakeMeeting();

        var actual = meetingFilter.IsValidFor(meeting);

        Assert.True(actual);
    }

    [Fact]
    public void IsValidFor_StartDateNotInRange_ReturnsFalse()
    {
        var meetingFilter = new MeetingFilter() { StartDate = _startDate.AddHours(1), EndDate = _endDate };
        var meeting = FakeMeeting();

        var actual = meetingFilter.IsValidFor(meeting);

        Assert.False(actual);
    }

    [Fact]
    public void IsValidFor_EndDateNotInRange_ReturnsFalse()
    {
        var meetingFilter = new MeetingFilter() { StartDate = _startDate, EndDate = _endDate.AddHours(-1) };
        var meeting = FakeMeeting();

        var actual = meetingFilter.IsValidFor(meeting);

        Assert.False(actual);
    }

    [Fact]
    public void IsValidFor_DateNotInRange_ReturnsFalse()
    {
        var meetingFilter = new MeetingFilter() { StartDate = _startDate.AddHours(1), EndDate = _endDate.AddHours(-1) };
        var meeting = FakeMeeting();

        var actual = meetingFilter.IsValidFor(meeting);

        Assert.False(actual);
    }

    [Fact]
    public void IsValidFor_EndDateSet_ReturnsTrue()
    {
        var meetingFilter = new MeetingFilter() { EndDate = _endDate.AddHours(1), };
        var meeting = FakeMeeting();

        var actual = meetingFilter.IsValidFor(meeting);

        Assert.True(actual);
    }

    [Fact]
    public void IsValidFor_EndDateSet_ReturnsFalse()
    {
        var meetingFilter = new MeetingFilter() { EndDate = _endDate.AddHours(-1), };
        var meeting = FakeMeeting();

        var actual = meetingFilter.IsValidFor(meeting);

        Assert.False(actual);
    }

    [Fact]
    public void IsValidFor_StartDateSet_ReturnsTrue()
    {
        var meetingFilter = new MeetingFilter() { StartDate = _startDate.AddHours(-1), };
        var meeting = FakeMeeting();

        var actual = meetingFilter.IsValidFor(meeting);

        Assert.True(actual);
    }

    [Fact]
    public void IsValidFor_StartDateSet_ReturnsFalse()
    {
        var meetingFilter = new MeetingFilter() { StartDate = _startDate.AddHours(1), };
        var meeting = FakeMeeting();

        var actual = meetingFilter.IsValidFor(meeting);

        Assert.False(actual);
    }

    [Fact]
    public void IsValidFor_CategorySet_ReturnsTrue()
    {
        var meetingFilter = new MeetingFilter() { Category = _category };
        var meeting = FakeMeeting();

        var actual = meetingFilter.IsValidFor(meeting);

        Assert.True(actual);
    }

    [Fact]
    public void IsValidFor_CategorySet_ReturnsFalse()
    {
        var meetingFilter = new MeetingFilter() { Category = _category + 1 };
        var meeting = FakeMeeting();

        var actual = meetingFilter.IsValidFor(meeting);

        Assert.False(actual);
    }

    [Fact]
    public void IsValidFor_TypeSet_ReturnsTrue()
    {
        var meetingFilter = new MeetingFilter() { Type = _type };
        var meeting = FakeMeeting();

        var actual = meetingFilter.IsValidFor(meeting);

        Assert.True(actual);
    }

    [Fact]
    public void IsValidFor_TypeSet_ReturnsFalse()
    {
        var meetingFilter = new MeetingFilter() { Type = _type + 1 };
        var meeting = FakeMeeting();

        var actual = meetingFilter.IsValidFor(meeting);

        Assert.False(actual);
    }

    [Fact]
    public void IsValidFor_KeyWordsSet_ReturnsFalse()
    {
        string[] words = _description.Split(" ");
        var meetingFilter = new MeetingFilter() { Keywords = new List<string>() { words[0] + words[1] } };
        var meeting = FakeMeeting();

        var actual = meetingFilter.IsValidFor(meeting);

        Assert.False(actual);
    }

    [Fact]
    public void IsValidFor_KeyWordsSet_ReturnsTrue()
    {
        string[] words = _description.Split(" ");
        var meetingFilter = new MeetingFilter() { Keywords = new List<string>() { words[0] } };
        var meeting = FakeMeeting();

        var actual = meetingFilter.IsValidFor(meeting);

        Assert.True(actual);
    }

    [Fact]
    public void IsValidFor_KeyWordsSetWithValidAndInvalidWords_ReturnsTrue()
    {
        string[] words = _description.Split(" ");
        var meetingFilter = new MeetingFilter() { Keywords = new List<string>() { "INVALID", words[0] } };
        var meeting = FakeMeeting();

        var actual = meetingFilter.IsValidFor(meeting);

        Assert.True(actual);
    }

    [Fact]
    public void IsValidFor_NumberOfAttendees_ReturnsFalse()
    {
        string[] words = _description.Split(" ");
        var meetingFilter = new MeetingFilter() { NumberOfAttendees = _attendees.Count + 1 };
        var meeting = FakeMeeting();

        var actual = meetingFilter.IsValidFor(meeting);

        Assert.False(actual);
    }

    private Meeting FakeMeeting()
    {
        var meeting = new Meeting(_meetingName, _responsiblePerson, _description, _category, _type, _startDate,
            _endDate);
        meeting.Attendees = _attendees;
        return meeting;
    }
}