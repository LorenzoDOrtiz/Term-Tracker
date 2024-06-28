using System.Collections.ObjectModel;

namespace TermTracker.CoreBusiness.Models;
public abstract class Alert
{
    public int CourseId { get; set; }
    public int Id { get; set; }
    public int ReminderValue { get; set; }
    public ObservableCollection<string> ReminderUnits { get; set; } = new ObservableCollection<string> { "seconds", "minutes", "hours", "days", "weeks" };
    public string ReminderUnit { get; set; }
}

public class StartDateAlert : Alert { }

public class EndDateAlert : Alert { }
