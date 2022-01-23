using System.Collections.Generic;
using VismaTask.Models.Meetings;

namespace VismaTask.Services.Meetings;

public class MeetingService : IMeetingService
{
    public bool IsResponsablePerson(string user, int meetingId, List<Meeting> meetings)
    {
        int index = meetings.FindIndex(m => m.MeetingId == meetingId);
        if(index >= 0)
            return meetings[index].ResponsiblePerson.CompareTo(user) == 0;
        return false;
    }
}