using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Text.Json;
using VismaTask.Models.Meetings;

namespace VismaTask.Repositories.Meetings;

public class MeetingRepository : IMeetingRepository
{
    private readonly string _filePath;
    private readonly JsonSerializerOptions _serializerOptions;

    public MeetingRepository(string filePath)
    {
        _serializerOptions = new JsonSerializerOptions() { WriteIndented = true };
        
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
        return Deserialize<List<Meeting>>(fileContent);
    }

    public Meeting Create(Meeting meetingToAdd)
    {
        var meetings = GetAll();
        meetings.Add(meetingToAdd);
        string jsonString = Serialize(meetings);
        File.WriteAllText(_filePath, jsonString);
        return meetingToAdd;
    }

    private string Serialize<T>(T objToSerialize)
    {
        return JsonSerializer.Serialize(objToSerialize, _serializerOptions);
    }

    private T Deserialize<T>(string jsonString) where T : new()
    {
        return JsonSerializer.Deserialize<T>(jsonString) ?? new T();
    }
}