using System.Collections.Generic;
using VismaTask.Models.Meetings;

namespace VismaTask.Repositories.Meetings;

public interface IMeetingRepository
{
    List<Meeting> GetAll();
}