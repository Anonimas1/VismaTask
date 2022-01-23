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
    
    public Meeting Create(Meeting meeting)
    {
        return _meetingRepository.Create(meeting);
    }

    public void Delete(int id)
    {
        var meetings = _meetingRepository.GetAll();
        if (_meetingService.IsResponsablePerson(_user, id, meetings))
            _meetingRepository.Delete(id);
    }
    
    public string 
    
    
}