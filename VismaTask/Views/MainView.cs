using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using VismaTask.Controllers;
using VismaTask.Models.Meetings;

namespace VismaTask.Views;

public class MainView
{
    private const int BorderLenght = 30;
    private readonly MeetingController _controller;
    public MainView(MeetingController controller)
    {
        _controller = controller;
    }

    public void ShowView()
    {
        while (true)
        {
            PrintOptions();
            int choice = GetNumberFromConsole();
            TakeAction(choice);
        }
    }
    
    
    public int GetNumberFromConsole()
    {
        while (true)
        {
            if (Int32.TryParse(Console.ReadLine(), out int value))
            {
                return value;
            }
            Console.WriteLine("Only numbers accepted");
        }
    }

    public string GetStringFromConsole()
    {
        return Console.ReadLine();
    }

    public void PrintOptions()
    {
        string border = new string('=', BorderLenght);
        Console.WriteLine("Press ENTER to continue");
        Console.ReadLine();
        Console.WriteLine();
        Console.WriteLine(border);
        Console.WriteLine("Choose an operation:");
        Console.WriteLine("1. List all meetings");
        Console.WriteLine("2. Delete a meeting");
        Console.WriteLine("3. Add a person to a meeting");
        Console.WriteLine("4. Remove a person from a meeting");
        Console.WriteLine("5. Create a meeting");
        Console.WriteLine("Enter a meeting id:");
    }

    public void ListAllMeetings()
    {
        List<Meeting> meetings = _controller.GetAll(new MeetingFilter());
        string border = new string('-', BorderLenght);
        foreach (var meeting in meetings)
        {
            Console.WriteLine(border);
            Console.Write(meeting);
            Console.WriteLine(border);
            Console.WriteLine();
        }
    }

    public void DeleteMeeting()
    {
        Console.WriteLine("Delete a meeting");
        Console.WriteLine("Enter a meeting id:");
        int id = GetNumberFromConsole();
        string result = _controller.Delete(id);
        Console.Write(result);
    }

    public void AddPerson()
    {
        Console.WriteLine("Add a person");
        Console.WriteLine("Enter a meeting id:");
        int id = GetNumberFromConsole();
        
        Console.WriteLine();
        Console.WriteLine(new string('-', BorderLenght));
        Console.WriteLine("Selected meeting: ");
        Console.Write(_controller.Get(id));
        Console.WriteLine(new string('-', BorderLenght));
        Console.WriteLine();
        
        Console.WriteLine("Enter a persons name:");
        string name = GetStringFromConsole();
        string result = _controller.AddPerson(id, name);
        Console.Write(result);
    }

    public void RemovePerson()
    {
        Console.WriteLine("Remove a person");
        Console.WriteLine("Enter a meeting id:");
        int id = GetNumberFromConsole();
        
        Console.WriteLine();
        Console.WriteLine(new string('-', BorderLenght));
        Console.WriteLine("Selected meeting: ");
        Console.Write(_controller.Get(id));
        Console.WriteLine(new string('-', BorderLenght));
        Console.WriteLine();
        
        Console.WriteLine("Enter a persons name:");
        string name = GetStringFromConsole();
        string result = _controller.RemovePerson(id, name);
        Console.Write(result);
    }

    public void CreateMeeting()
    {
        Console.WriteLine("Create a meeting");
        
        Console.WriteLine("Enter a meeting name:");
        string name = GetStringFromConsole();
        
        Console.WriteLine("Enter a description:");
        string description = GetStringFromConsole();
        
        Console.WriteLine("Enter a type(Live, InPerson):");
        MeetingType type;
        while (!Enum.TryParse(GetStringFromConsole(), out type)){ }
        
        Console.WriteLine("Enter a category(CodeMonkey, Hub, Short, TeamBuilding):");
        MeetingCategory category;
        while (!Enum.TryParse(GetStringFromConsole(), out category)){ }
        
        Console.WriteLine("Enter a start date:");
        DateTime startDate = GetDateFormConsole();
        
        Console.WriteLine("Enter a end date:");
        DateTime endDate = GetDateFormConsole();

        Meeting meeting = new Meeting(name, description, category, type, startDate, endDate);
        var result = _controller.Create(meeting);
        
        Console.WriteLine();
        Console.WriteLine(new string('-', BorderLenght));
        Console.WriteLine("Created meeting: ");
        Console.Write(result);
        Console.WriteLine(new string('-', BorderLenght));
    }

    public DateTime GetDateFormConsole()
    {
        DateTime result;
        Console.WriteLine("Enter a date(Format: YYYY-MM-DDTHH:MM"); 
        while (!DateTime.TryParse(GetStringFromConsole(), out result))
        {
            Console.WriteLine("Enter a date(Format: YYYY-MM-DDTHH:MM"); 
        }

        return result;
    }

    public void TakeAction(int choice)
    {
        switch (choice)
        {
            case 1 : 
                ListAllMeetings();
                break;
            case 2 :
                DeleteMeeting();
                break;
            case 3:
                AddPerson();
                break;
            case 4:
                RemovePerson();
                break;
            case 5:
                CreateMeeting();
                break;
            default:
                Console.WriteLine("No such option");
                break;
        }
    }
}