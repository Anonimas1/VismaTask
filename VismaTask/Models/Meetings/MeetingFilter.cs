using System;
using System.Collections.Generic;

namespace VismaTask.Models.Meetings;

public class MeetingFilter
{
    public List<string> Keywords { get; set; } = new List<string>();
    public string ResponsiblePerson { get; set; } = "";
    public MeetingCategory? Category { get; set; } = null;
    public MeetingType? Type { get; set; } = null;
    public DateTime? StartDate { get; set; } = null;
    public DateTime? EndDate { get; set; } = null;
    public int NumberOfAttendees { get; set; } = 0;

    public bool IsValidFor(Meeting meeting)
    {
        bool matchesPerson = true;
        if (ResponsiblePerson.Length > 0)
            matchesPerson = meeting.ResponsiblePerson.CompareTo(ResponsiblePerson) == 0;

        return HasAnyKeyword(meeting.Description) &&
               matchesPerson &&
               MatchesCategory(meeting.Category) &&
               MatchesType(meeting.Type) &&
               DateInRange(meeting.StartDate, meeting.EndDate) &&
               meeting.Attendees.Count > NumberOfAttendees;
    }

    private bool DateInRange(DateTime startDate, DateTime endDate)
    {
        if (StartDate == null && EndDate == null)
            return true;

        if (StartDate != null && EndDate != null)
            return startDate >= StartDate && endDate <= EndDate;

        if (StartDate != null)
            return startDate >= StartDate;

        return endDate <= EndDate;
    }

    private bool HasAnyKeyword(string stringToCheck)
    {
        if (Keywords.Count == 0)
            return true;
        
        foreach (var keyword in Keywords)
        {
            if (stringToCheck.Contains(keyword))
                return true;
        }

        return false;
    }

    private bool MatchesCategory(MeetingCategory category)
    {
        if (Category == null)
            return true;
        return Category == category;
    }

    private bool MatchesType(MeetingType type)
    {
        if (Type == null)
            return true;
        return Type == type;
    }
}