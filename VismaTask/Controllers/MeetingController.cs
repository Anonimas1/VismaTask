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
        return _meetingRepository.Create(meeting);
    }

    public void Delete(int id)
    {
        var meeting = _meetingRepository.Get(id);
        if (meeting.ResponsiblePerson.CompareTo(_user) == 0)
            _meetingRepository.Delete(id);
    }

    public string RemovePerson(int id, string person)
    {
        var meeting = _meetingRepository.Get(id);
        if (meeting.ResponsiblePerson.CompareTo(_user) != 0)
        {
            _meetingRepository.RemovePerson(id, person);
            return "";
        }
        return "Could not remove person, person is the one responsible for the meeting";
    }


}