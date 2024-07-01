using System.Collections.ObjectModel;

namespace TermTracker.CoreBusiness.Models
{
    public class Assessment
    {
        public int CourseId { get; set; }
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ObservableCollection<StartDateAlert> StartDateAlerts { get; set; } = new();
        public ObservableCollection<EndDateAlert> EndDateAlerts { get; set; } = new();
    }
}
