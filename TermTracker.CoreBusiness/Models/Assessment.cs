using System.Collections.ObjectModel;

namespace TermTracker.CoreBusiness.Models
{
    public abstract class Assessment
    {
        public int CourseId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ObservableCollection<Alert> Alerts { get; set; } = new ObservableCollection<Alert>();
    }

    public class ObjectiveAssessment : Assessment
    {
    }

    public class PerformanceAssessment : Assessment
    {
    }
}
