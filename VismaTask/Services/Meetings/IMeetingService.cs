using System.Collections.Generic;
using VismaTask.Models.Meetings;

namespace VismaTask.Services.Meetings;

public interface IMeetingService
{
    bool PersonHasIntersectingMeetings(string person, Meeting meetingToAddTo, IEnumerable<Meeting> meetings);
}