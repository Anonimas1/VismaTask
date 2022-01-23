using System;
using System.Collections.Generic;
using VismaTask.Models.Meetings;
using VismaTask.Repositories.Meetings;

namespace VismaTaskTest.Stubs;

public class MeetingRepositoryStub : IMeetingRepository
{
    public List<Meeting> GetAll()
    {
        return new List<Meeting>()
        {
            new Meeting(
                "First",
                "First responsible",
                "First description",
                MeetingCategory.Hub,
                MeetingType.Live,
                new DateTime(2000, 01, 01),
                new DateTime(2000, 01, 02)
                ),
            new Meeting(
                "Second",
                "Second responsible",
                "Second description",
                MeetingCategory.Short,
                MeetingType.Live,
                new DateTime(2001, 01, 01),
                new DateTime(2001, 01, 02)
            ),
            new Meeting(
                "Third",
                "Third responsible",
                "Third description",
                MeetingCategory.Short,
                MeetingType.Live,
                new DateTime(2002, 01, 01),
                new DateTime(2002, 01, 02)
            ),
            new Meeting(
                "Fourth",
                "Fourth responsible",
                "Fourth description",
                MeetingCategory.Short,
                MeetingType.InPerson,
                new DateTime(2002, 01, 01),
                new DateTime(2002, 01, 02)
            )
        };

    }

    public Meeting Create(Meeting meetingToAdd)
    {
        return meetingToAdd;
    }
}