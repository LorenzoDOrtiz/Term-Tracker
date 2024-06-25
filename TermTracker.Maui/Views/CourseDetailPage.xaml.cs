using TermTracker.CoreBusiness.Models;
using TermTracker.Maui.ViewModels;

namespace TermTracker.Maui.Views;

[QueryProperty(nameof(Course), "Course")]
public partial class CourseDetailPage : ContentPage
{

    private readonly CourseViewModel courseViewModel;

    private Course _course;

    public CourseDetailPage(CourseViewModel courseViewModel)
    {
        InitializeComponent();
        this.courseViewModel = courseViewModel;

        this.BindingContext = courseViewModel;
    }

    public Course Course
    {
        get => _course;
        set
        {
            _course = value;
            courseViewModel.Course = _course;
            OnPropertyChanged(nameof(Course));
        }
    }
}