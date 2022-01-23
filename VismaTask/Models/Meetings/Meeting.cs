using System;
using System.Collections.Generic;

namespace VismaTask.Models.Meetings;

public class Meeting
{
    public Meeting(){}
    public Meeting(string name,
        string description,
        MeetingCategory category,
        MeetingType type,
        DateTime startDate,
        DateTime endDate)
    {
        Name = name;
        Description = description;
        Category = category;
        Type = type;
        StartDate = startDate;
        EndDate = endDate;
        Attendees = new List<string>();
    }
    public int MeetingId { get; set; }
    public string Name { get; set; }
    public string ResponsiblePerson { get; set; }
    public string Description { get; set; }
    public MeetingCategory Category { get; set; }
    public MeetingType Type { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<string> Attendees { get; set; }


    public bool Intersects(Meeting meeting)
    {
        return meeting.StartDate > StartDate && meeting.StartDate < EndDate ||
               meeting.EndDate > StartDate && meeting.EndDate < EndDate;
    }

    public override string ToString()
    {
        string resultString = $"Meeting id: {MeetingId}\n" +
                              $"Meeting name: {Name}\n" +
                              $"Responsible person: {ResponsiblePerson}\n" +
                              $"Description: {Description}\n" +
                              $"Category: {Category}\n" +
                              $"Type: {Type}\n" +
                              $"Start date: {StartDate}\n" +
                              $"End date: {EndDate}\n" +
                              "Atendees:\n";
        foreach (var attendee in Attendees)
        {
            resultString = resultString + $"     {attendee}\n";
        }
        return resultString;
    }
}