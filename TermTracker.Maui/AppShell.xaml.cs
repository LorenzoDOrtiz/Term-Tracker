using CommunityToolkit.Mvvm.Messaging;
using TermTracker.Maui.ViewModels;
using TermTracker.Maui.Views;
using static TermTracker.Maui.ViewModels.TermViewModel;

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

        // So I guess MAUI still doesn't support automatic databinding updates with the shell, so we'll have to do this manually 
        WeakReferenceMessenger.Default.Register<TermSavedMessage>(this, async (r, m) =>
        {
            await termsViewModel.LoadTermsAsync();
        });
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await this.termsViewModel.LoadTermsAsync();
    }
}
