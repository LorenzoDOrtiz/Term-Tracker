namespace TermTracker.CoreBusiness.Models;
public class Course
{
    public int TermId { get; set; }
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Instructor? Instructor { get; set; }
}