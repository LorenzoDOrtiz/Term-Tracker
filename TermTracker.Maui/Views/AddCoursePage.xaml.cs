using TermTracker.CoreBusiness.Models;
using TermTracker.Maui.ViewModels;

namespace TermTracker.Maui.Views;

[QueryProperty(nameof(TermId), "TermId")]
public partial class AddCoursePage : ContentPage
{
    private readonly CourseViewModel courseViewModel;
    private int _termId;


    public AddCoursePage(CourseViewModel courseViewModel)
    {
        InitializeComponent();
        this.courseViewModel = courseViewModel;
        this.BindingContext = this.courseViewModel;
    }
    public int TermId
    {
        get => _termId;
        set
        {
            _termId = value;
            OnPropertyChanged(nameof(TermId));
        }
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        this.courseViewModel.Course = new Course
        {
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddMonths(6),
            TermId = _termId,
        };
    }
}