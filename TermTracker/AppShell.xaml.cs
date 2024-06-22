using System.Collections.ObjectModel;
using TermTracker.CoreBusiness;
using TermTracker.Maui.ViewModels;
using TermTracker.UseCases.Interfaces;
using TermTracker.UseCases.PluginInterfaces;

namespace TermTracker.Maui;

public partial class AppShell : Shell
{
    private readonly TermViewModel termViewModel;
    public AppShell(TermViewModel termViewModel)
    {
        InitializeComponent();
        this.termViewModel = termViewModel;

        this.BindingContext = termViewModel;

        SetFlyoutWidth();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await this.termViewModel.LoadTerms();
    }

    private void SetFlyoutWidth()
    {
        // Get the main display info
        var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

        // Calculate the flyout width as half of the screen width
        double flyoutWidth = mainDisplayInfo.Width / mainDisplayInfo.Density / 2;

        // Set the FlyoutWidth property
        this.FlyoutWidth = flyoutWidth;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;

        // Set the text color to light gray when it's clicked
        button.TextColor = Colors.LightGray;

        // Delay setting the text color back to transparent
        button.Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(200), () =>
        {
            button.TextColor = Colors.Black;
        });
    }

}
