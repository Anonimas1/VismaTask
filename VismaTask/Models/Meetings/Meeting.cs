using System;
using System.Collections.Generic;

namespace VismaTask.Models.Meetings;

public class Meeting
{
    public Meeting(){}
    public Meeting(string name,
        string responsiblePerson,
        string description,
        MeetingCategory category,
        MeetingType type,
        DateTime startDate,
        DateTime endDate)
    {
        Name = name;
        ResponsiblePerson = responsiblePerson;
        Description = description;
        Category = category;
        Type = type;
        StartDate = startDate;
        EndDate = endDate;
        Attendees = new List<string>();
    }

    public string Name { get; set; }
    public string ResponsiblePerson { get; set; }
    public string Description { get; set; }
    public MeetingCategory Category { get; set; }
    public MeetingType Type { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<string> Attendees { get; set; }
}