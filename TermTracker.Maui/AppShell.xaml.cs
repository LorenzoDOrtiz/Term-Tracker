using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using TermTracker.CoreBusiness;
using TermTracker.Maui.ViewModels;
using TermTracker.Maui.Views;
using TermTracker.UseCases.Interfaces;
using TermTracker.UseCases.PluginInterfaces;
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

        // So I guess MAUI still doesn't support automatic databinding updates with the shell, so we'll have to do this manually 
        WeakReferenceMessenger.Default.Register<TermSavedMessage>(this, (r, m) =>
        {
            termsViewModel.LoadTermsAsync();
        });
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await this.termsViewModel.LoadTermsAsync();
    }
}
