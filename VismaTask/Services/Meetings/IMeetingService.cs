using System.Collections.Generic;
using VismaTask.Models.Meetings;

namespace VismaTask.Services.Meetings;

public interface IMeetingService
{
    bool PersonHasIntersectingMeetings(string person, int meetingId, List<Meeting> meetings);
}