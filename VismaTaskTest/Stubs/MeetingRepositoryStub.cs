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
                "Admin",
                "First description",
                MeetingCategory.Hub,
                MeetingType.Live,
                new DateTime(2000, 01, 01),
                new DateTime(2000, 01, 02)
            ){MeetingId = 0},
            new Meeting(
                "Second",
                "Second responsible",
                "Second description",
                MeetingCategory.Short,
                MeetingType.Live,
                new DateTime(2001, 01, 01),
                new DateTime(2001, 01, 02)
            ){MeetingId = 1},
            new Meeting(
                "Third",
                "Third responsible",
                "Third description",
                MeetingCategory.Short,
                MeetingType.Live,
                new DateTime(2002, 01, 01),
                new DateTime(2002, 01, 02)
            ){MeetingId = 2},
            new Meeting(
                "Fourth",
                "Admin",
                "Fourth description",
                MeetingCategory.Short,
                MeetingType.InPerson,
                new DateTime(2002, 01, 01),
                new DateTime(2002, 01, 02)
            ){MeetingId = 3}
        };
    }
    public List<Meeting> GetAll()
    {
        return _meetings;

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
}