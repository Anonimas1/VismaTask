using System;
using System.Collections.Generic;
using System.Linq;
using VismaTask.Models.Meetings;
using VismaTask.Repositories.Meetings;

namespace VismaTask.Controllers;

public class MeetingController
{
    private readonly IMeetingRepository _meetingRepository;
    
    public MeetingController(IMeetingRepository meetingRepository)
    {
        _meetingRepository = meetingRepository;
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
}