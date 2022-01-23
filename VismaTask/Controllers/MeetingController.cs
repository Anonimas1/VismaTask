using System;
using System.Collections.Generic;
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
    
    
    public List<Meeting> GetAll()
    {
        return _meetingRepository.GetAll();
    }
    
    public Meeting Create(Meeting meeting)
    {
        return _meetingRepository.Create(meeting);
    }
}