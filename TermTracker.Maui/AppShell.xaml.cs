using TermTracker.Maui.ViewModels;
using TermTracker.Maui.Views;

namespace TermTracker.Maui;

public partial class AppShell : Shell
{
    private readonly TermsViewModel termsViewModel;
    public AppShell(TermsViewModel termsViewModel)
    {
        InitializeComponent();
        this.termsViewModel = termsViewModel;

        this.BindingContext = termsViewModel;

        Routing.RegisterRoute(nameof(AddTermPage), typeof(AddTermPage));
        Routing.RegisterRoute(nameof(EditTermPage), typeof(EditTermPage));
        Routing.RegisterRoute(nameof(TermCoursesPage), typeof(TermCoursesPage));
        Routing.RegisterRoute(nameof(AddCoursePage), typeof(AddCoursePage));
        Routing.RegisterRoute(nameof(CourseDetailPage), typeof(CourseDetailPage));
        Routing.RegisterRoute(nameof(EditCoursePage), typeof(EditCoursePage));
        Routing.RegisterRoute(nameof(CourseAlertPage), typeof(CourseAlertPage));
        Routing.RegisterRoute(nameof(CourseNotesPage), typeof(CourseNotesPage));


    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await this.termsViewModel.LoadTermsAsync();
    }
}
