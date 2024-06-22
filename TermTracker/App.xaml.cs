namespace TermTracker.Maui;

public partial class App : Application
{
    private readonly AppShell _appShell;

    public App(AppShell appShell)
    {
        InitializeComponent();

        this._appShell = appShell;

        MainPage = _appShell;
    }
}
