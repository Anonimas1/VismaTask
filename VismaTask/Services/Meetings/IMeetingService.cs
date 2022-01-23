using System.Collections.Generic;
using VismaTask.Models.Meetings;

namespace VismaTask.Services.Meetings;

public interface IMeetingService
{
    bool IsResponsablePerson(string user, int meetingId, List<Meeting> meetings);
}