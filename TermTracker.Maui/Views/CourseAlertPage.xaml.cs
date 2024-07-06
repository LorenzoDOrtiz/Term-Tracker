using TermTracker.Maui.ViewModels;

namespace TermTracker.Maui.Views;

public partial class CourseAlertPage : ContentPage
{
    private readonly CourseViewModel courseViewModel;

    public CourseAlertPage(CourseViewModel courseViewModel)
    {
        InitializeComponent();
        this.courseViewModel = courseViewModel;

        this.BindingContext = this.courseViewModel;
    }
};