using System;
using System.Collections.Generic;
using VismaTask.Models.Meetings;

namespace VismaTask.Services.Meetings;

public class MeetingService : IMeetingService
{
    public bool PersonHasIntersectingMeetings(string person, int meetingId, List<Meeting> meetings)
    {
        int index = meetings.FindIndex(m => m.MeetingId == meetingId);
        var meetingToAddPersonTo = meetings[index];
        for (int i = 0; i < meetings.Count; i++)
        {
            if(meetings[i].Attendees.Contains(person)) 
                if (meetingToAddPersonTo.Intersects(meetings[i]) && i != index)
                    return true;
        }

        return false;
    }
    
}