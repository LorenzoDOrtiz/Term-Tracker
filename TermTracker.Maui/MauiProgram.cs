using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using TermTracker.Maui.Interfaces;
using TermTracker.Maui.Services;
using TermTracker.Maui.ViewModels;
using TermTracker.Maui.Views;
using TermTracker.Plugins.DataStore.InMemory;
using TermTracker.UseCases.CourseUseCases;
using TermTracker.UseCases.Interfaces;
using TermTracker.UseCases.PluginInterfaces;
using TermTracker.UseCases.TermUseCases;

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
        builder.UseMauiApp<App>().UseMauiCommunityToolkit();
#if DEBUG
        builder.Logging.AddDebug();
#endif

        //Repsitory Interfaces
        builder.Services.AddSingleton<ITermRepository, TermInMemoryRepository>();
        builder.Services.AddSingleton<ICourseRepository, CourseInMemoryRepository>();

        // Interfaces
        builder.Services.AddSingleton<IViewTermsUseCase, ViewTermsUseCase>();
        builder.Services.AddTransient<IViewTermUseCase, ViewTermUseCase>();

        builder.Services.AddTransient<IEditTermUseCase, EditTermUseCase>();
        builder.Services.AddTransient<IAddTermUseCase, AddTermUseCase>();
        builder.Services.AddTransient<IDeleteTermUseCase, DeleteTermUseCase>();


        builder.Services.AddSingleton<IViewCoursesUseCase, ViewCoursesUseCase>();

        builder.Services.AddTransient<IAddCourseUseCase, AddCourseUseCase>();

        builder.Services.AddTransient<IAlertService, AlertService>();

        // Pages
        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddSingleton<AddTermPage>();
        builder.Services.AddSingleton<EditTermPage>();

        builder.Services.AddSingleton<TermCoursesPage>();
        builder.Services.AddSingleton<AddCoursePage>();


        // ViewModels
        builder.Services.AddSingleton<TermsViewModel>();
        builder.Services.AddSingleton<TermViewModel>();

        builder.Services.AddSingleton<CoursesViewModel>();
        builder.Services.AddSingleton<CourseViewModel>();

        return builder.Build();
    }
}
