using System.Collections.Generic;
using VismaTask.Models.Meetings;

namespace VismaTask.Repositories.Meetings;

public interface IMeetingRepository
{
    List<Meeting> GetAll();
    Meeting Get(int id);
    Meeting Create(Meeting meetingToAdd);
    void Delete(int id);
    void RemovePerson(int id, string person);
    void AddPerson(int id, string person);
}