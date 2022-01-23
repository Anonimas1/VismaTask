using System;
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

    public Meeting Get(int id)
    {
        var meetings = GetAll();
        var index = GetIndexInCollection(id, meetings);
        return meetings[index];
    }

    public Meeting Create(Meeting meetingToAdd)
    {
        var meetings = GetAll();
        int id = meetings.Count > 0 ? meetings[meetings.Count - 1].MeetingId + 1 : 1;
        meetingToAdd.MeetingId = id;
        meetings.Add(meetingToAdd);
        WriteToFile(_filePath, meetings);
        return meetingToAdd;
    }

    public void Delete(int id)
    {
        var meetings = GetAll();
        int indexInArray = GetIndexInCollection(id, meetings);
        meetings.RemoveAt(indexInArray);
        WriteToFile(_filePath, meetings);
    }

    public void RemovePerson(int id, string person)
    {
        var meetings = GetAll();
        int indexInArray = GetIndexInCollection(id, meetings);
        meetings[indexInArray].Attendees.Remove(person);
        WriteToFile(_filePath, meetings);
    }

    public void AddPerson(int id, string person)
    {
        var meetings = GetAll();
        int indexInArray = GetIndexInCollection(id, meetings);
        meetings[indexInArray].Attendees.Add(person);
        WriteToFile(_filePath, meetings);
    }

    private string Serialize<T>(T objToSerialize)
    {
        return JsonSerializer.Serialize(objToSerialize, _serializerOptions);
    }

    private T Deserialize<T>(string jsonString) where T : new()
    {
        return JsonSerializer.Deserialize<T>(jsonString) ?? new T();
    }

    private void WriteToFile<T>(string fileName, T content)
    {
        string jsonString = Serialize(content);
        File.WriteAllText(fileName, jsonString);
    }

    private int GetIndexInCollection(int meetingId, List<Meeting> meetings)
    {
        return meetings.FindIndex(m => m.MeetingId == meetingId);
    }
}