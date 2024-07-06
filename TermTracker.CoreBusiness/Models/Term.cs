namespace TermTracker.CoreBusiness.Models;

// All the code in this file is included in all platforms.
public class Term
{
    public int TermId { get; set; }
    public string TermName { get; set; }
    public DateTime TermStartDate { get; set; }
    public DateTime TermEndDate { get; set; }
}
