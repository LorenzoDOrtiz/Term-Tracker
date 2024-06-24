using CommunityToolkit.Mvvm.ComponentModel;

namespace TermTracker.CoreBusiness.Models;
public partial class Course : ObservableObject
{
    [ObservableProperty]
    public int termId;

    [ObservableProperty]
    public int courseId;

    [ObservableProperty]
    public string courseName;

    [ObservableProperty]
    public DateTime courseStartDate;

    [ObservableProperty]
    public DateTime courseEndDate;
}