namespace TermTracker.CoreBusiness.Models;
public partial class Course
{
    public int TermId { get; set; }
    public int CourseId { get; set; }
    public string CourseName { get; set; }
    public DateTime CourseStartDate { get; set; }
    public DateTime CourseEndDate { get; set; }
}