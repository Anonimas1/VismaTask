using System;
using System.Collections.Generic;
using VismaTask.Models.Meetings;
using VismaTask.Repositories.Meetings;

namespace VismaTaskTest.Stubs;

public class MeetingRepositoryStub : IMeetingRepository
{
    private List<Meeting> _meetings;
    public MeetingRepositoryStub()
    {
        _meetings = new List<Meeting>()
        {
            new Meeting(
                "First",
                "First description",
                MeetingCategory.Hub,
                MeetingType.Live,
                new DateTime(2000, 01, 01),
                new DateTime(2000, 01, 02)
            ){MeetingId = 0, ResponsiblePerson = "Admin"},
            new Meeting(
                "Second",
                "Second description",
                MeetingCategory.Short,
                MeetingType.Live,
                new DateTime(2001, 01, 01),
                new DateTime(2001, 01, 02)
            ){MeetingId = 1, ResponsiblePerson = "Second responsible"},
            new Meeting(
                "Third",
                "Third description",
                MeetingCategory.Short,
                MeetingType.Live,
                new DateTime(2002, 01, 01),
                new DateTime(2002, 01, 02)
            ){MeetingId = 2, ResponsiblePerson = "Third responsible"},
            new Meeting(
                "Fourth",
                "Fourth description",
                MeetingCategory.Short,
                MeetingType.InPerson,
                new DateTime(2002, 01, 01),
                new DateTime(2002, 01, 02)
            ){MeetingId = 3, ResponsiblePerson = "Admin"}
        };
    }
    public List<Meeting> GetAll()
    {
        return _meetings;

    }

    public Meeting Get(int id)
    {
        int indexInArray = _meetings.FindIndex(m => m.MeetingId == id);
        return _meetings[indexInArray];
    }

    public Meeting Create(Meeting meetingToAdd)
    {
        return meetingToAdd;
    }

    public void Delete(int id)
    {
        int indexInArray = _meetings.FindIndex(m => m.MeetingId == id);
        _meetings.RemoveAt(indexInArray);
    }

    public void RemovePerson(int id, string person)
    {
        int indexInArray = _meetings.FindIndex(m => m.MeetingId == id);
        _meetings[indexInArray].Attendees.Remove(person);
    }

    public void AddPerson(int id, string person)
    {
        int indexInArray = _meetings.FindIndex(m => m.MeetingId == id);
        _meetings[indexInArray].Attendees.Add(person);
    }
}