using Microsoft.Extensions.Logging;
using TermTracker.Maui.ViewModels;
using TermTracker.Plugins.DataStore.InMemory;
using TermTracker.UseCases;
using TermTracker.UseCases.Interfaces;
using TermTracker.UseCases.PluginInterfaces;

namespace TermTracker.Maui;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif

        // Interfaces
        builder.Services.AddSingleton<ITermRepository, TermInMemoryRepository>();

        builder.Services.AddSingleton<IViewTermsUseCase, ViewTermsUseCase>();
        //builder.Services.AddSingleton<IEditTermsUseCase, IEditTermsUseCase>();
        //builder.Services.AddSingleton<IAddTermsUseCase, IAddTermsUseCase>();
        //builder.Services.AddSingleton<IDeleteTermsUseCase, IDeleteTermsUseCase>();

        // Pages
        builder.Services.AddSingleton<AppShell>();

        // ViewModels
        builder.Services.AddSingleton<TermViewModel>();



        return builder.Build();
    }
}
