using System;
using System.Collections.Generic;
using System.Threading.Channels;
using VismaTask.Controllers;
using VismaTask.Models.Meetings;
using VismaTask.Repositories.Meetings;
using VismaTask.Services.Meetings;
using VismaTask.Views;

namespace VismaTask;

class Program
{
    public static void Main(string[] args)
    {
        string user = GetName();
        IMeetingRepository repository = new MeetingRepository("meetings.json");
        IMeetingService meetingService = new MeetingService();
        
        var meetingController = new MeetingController(repository, meetingService, user);
        
        MainView mainView = new MainView(meetingController);
        mainView.ShowView();
    }

    public static string GetName()
    {
        string user;
        while (true)
        {
            Console.WriteLine("Enter your name:");
            user = Console.ReadLine();
            if (user.Length > 0)
                break;
        }

        return user;
    }
}