using TermTracker.Maui.ViewModels;

namespace TermTracker.Maui.Views;

[QueryProperty(nameof(CourseId), "Id")]

public partial class EditCoursePage : ContentPage
{
    private readonly CourseViewModel courseViewModel;

    public EditCoursePage(CourseViewModel courseViewModel)
    {
        InitializeComponent();
        this.courseViewModel = courseViewModel;

        this.BindingContext = courseViewModel;
    }
    public string CourseId
    {
        set
        {
            if (!string.IsNullOrWhiteSpace(value) && int.TryParse(value, out int courseId))
            {
                LoadCourse(courseId);
            }
        }
    }
    private async void LoadCourse(int courseId)
    {
        await this.courseViewModel.LoadCourse(courseId);
    }
}