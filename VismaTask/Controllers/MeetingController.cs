using System;
using System.Collections.Generic;
using System.Linq;
using VismaTask.Models.Meetings;
using VismaTask.Repositories.Meetings;
using VismaTask.Services.Meetings;

namespace VismaTask.Controllers;

public class MeetingController
{
    private readonly IMeetingRepository _meetingRepository;
    private readonly IMeetingService _meetingService;
    private readonly string _user;
    public MeetingController(IMeetingRepository meetingRepository, IMeetingService meetingService, string user)
    {
        _meetingRepository = meetingRepository;
        _user = user;
        _meetingService = meetingService;
    }
    
    
    public List<Meeting> GetAll(MeetingFilter filter)
    {
        var meetings =_meetingRepository.GetAll();
        meetings = meetings.Where(m => filter.IsValidFor(m)).ToList();
        return meetings;
    }

    public Meeting Get(int id)
    {
        return _meetingRepository.Get(id);
    }
    
    public Meeting Create(Meeting meeting)
    {
        meeting.ResponsiblePerson = _user;
        meeting.Attendees.Add(_user);
        return _meetingRepository.Create(meeting);
    }

    public void Delete(int id)
    public string Delete(int id)
    {
        var meeting = _meetingRepository.Get(id);
        if (meeting.ResponsiblePerson.CompareTo(_user) == 0)
        {
            _meetingRepository.Delete(id);
            return "Deleted\n";
        }

        return "Only the person responsible for the meeting can delete it\n";
    }

    public string RemovePerson(int id, string person)
    {
        var meeting = _meetingRepository.Get(id);
        if (meeting.ResponsiblePerson.CompareTo(_user) != 0)
        if (meeting.ResponsiblePerson.CompareTo(person) != 0)
        {
            _meetingRepository.RemovePerson(id, person);
            return "Removed\n";
        }
        return "Could not remove person, person is the one responsible for the meeting\n";
    }

    public string AddPerson(int id, string person)
    {
        var meeting = _meetingRepository.Get(id);
        var meetings = _meetingRepository.GetAll();
        if (!meeting.Attendees.Contains(person))
        {
            _meetingRepository.AddPerson(id, person);
            if (_meetingService.PersonHasIntersectingMeetings(person, meeting, meetings))
                return "This person has intersecting meetings\n";
            return "";
        }
        return "Could not remove person, person is the one responsible for the meeting";
        return "This person is already present\n";
    }


}