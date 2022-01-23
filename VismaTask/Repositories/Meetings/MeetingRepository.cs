using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using VismaTask.Models.Meetings;

namespace VismaTask.Repositories.Meetings;

public class MeetingRepository : IMeetingRepository
{
    private readonly string _filePath;

    public MeetingRepository(string filePath)
    {
        _filePath = filePath;
        if (!File.Exists(_filePath))
        {
            using (var stream = File.Create(_filePath))
            {
                var bytes = Encoding.UTF8.GetBytes("[]");
                stream.Write(bytes);
            }
        }
    }

    public List<Meeting> GetAll()
    {
        string fileContent = File.ReadAllText(_filePath);
        var meetings = JsonSerializer.Deserialize<List<Meeting>>(fileContent);
        return meetings ?? new List<Meeting>();
    }

    public Meeting Create(Meeting meetingToAdd)
    {
        var meetings = GetAll();
        meetings.Add(meetingToAdd);
        string jsonString = JsonSerializer.Serialize(meetings);
        File.WriteAllText(_filePath, jsonString);
        return meetingToAdd;
    }
}