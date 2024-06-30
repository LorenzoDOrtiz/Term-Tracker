using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Plugin.LocalNotification;
using TermTracker.CoreBusiness.Models;
using TermTracker.Maui.Interfaces;
using TermTracker.Maui.Services;
using TermTracker.Maui.ViewModels;
using TermTracker.Maui.Views;
using TermTracker.Plugins.DataStore.InMemory;
using TermTracker.UseCases.PluginInterfaces;
using TermTracker.UseCases.UseCaseImplementations;
using TermTracker.UseCases.UseCaseInterfaces;
using TermTracker.UseCases.UseCases;

namespace TermTracker.Maui;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseLocalNotification()
            .UseMauiCommunityToolkit()

            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
#if DEBUG
        builder.Logging.AddDebug();
#endif

        // Register the repositories and their interfaces as singletons
        builder.Services.AddSingleton<ICourseRepository<Course>, CourseInMemoryRepository>();
        builder.Services.AddSingleton<ITermRepository<Term>, TermInMemoryRepository>();

        // Interfaces
        builder.Services.AddTransient<IViewCollectionUseCase<Course>, ViewCollectionUseCase<Course>>();
        builder.Services.AddTransient<IViewCollectionUseCase<Term>, ViewCollectionUseCase<Term>>();

        builder.Services.AddTransient<IViewUseCase<Term>, ViewUseCase<Term>>();
        builder.Services.AddTransient<IViewUseCase<Course>, ViewUseCase<Course>>();

        builder.Services.AddTransient<IAddUseCase<Term>, AddUseCase<Term>>();
        builder.Services.AddTransient<IAddUseCase<Course>, AddUseCase<Course>>();

        builder.Services.AddTransient<IEditUseCase<Term>, EditUseCase<Term>>();
        builder.Services.AddTransient<IEditUseCase<Course>, EditUseCase<Course>>();

        builder.Services.AddTransient<IDeleteUseCase<Term>, DeleteUseCase<Term>>();
        builder.Services.AddTransient<IDeleteUseCase<Course>, DeleteUseCase<Course>>();

        builder.Services.AddTransient<IAlertService, AlertService>();

        // Pages
        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddSingleton<AddTermPage>();
        builder.Services.AddSingleton<EditTermPage>();
        builder.Services.AddSingleton<TermCoursesPage>();
        builder.Services.AddSingleton<AddCoursePage>();
        builder.Services.AddSingleton<CourseDetailPage>();
        builder.Services.AddSingleton<EditCoursePage>();
        builder.Services.AddSingleton<CourseAlertPage>();
        builder.Services.AddSingleton<CourseNotesPage>();
        builder.Services.AddSingleton<AssessmentAlertPage>();

        // ViewModels
        builder.Services.AddSingleton<TermsViewModel>();
        builder.Services.AddSingleton<TermViewModel>();
        builder.Services.AddSingleton<CoursesViewModel>();
        builder.Services.AddSingleton<CourseViewModel>();

        return builder.Build();
    }
}
