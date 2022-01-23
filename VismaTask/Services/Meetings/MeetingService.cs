using System;
using System.Collections.Generic;
using VismaTask.Models.Meetings;

namespace VismaTask.Services.Meetings;

public class MeetingService : IMeetingService
{
    public bool PersonHasIntersectingMeetings(string person, Meeting meetingToAddTo, IEnumerable<Meeting> meetings)
    {
        foreach (var meeting in meetings)
        {
            if(meeting.Attendees.Contains(person))
                if (meetingToAddTo.Intersects(meeting) && meetingToAddTo.MeetingId != meeting.MeetingId)
                    return true;
        }
        return false;
    }
    
}