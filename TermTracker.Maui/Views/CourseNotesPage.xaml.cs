using TermTracker.Maui.ViewModels;

namespace TermTracker.Maui.Views;

public partial class CourseNotesPage : ContentPage
{
    private readonly CourseViewModel courseViewModel;

    public CourseNotesPage(CourseViewModel courseViewModel)
    {
        InitializeComponent();
        this.courseViewModel = courseViewModel;

        this.BindingContext = this.courseViewModel;
    }
}