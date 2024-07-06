using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TermTracker.CoreBusiness.Models;
using TermTracker.Maui.Views;
using TermTracker.UseCases.UseCaseInterfaces;

namespace TermTracker.Maui.ViewModels;

[QueryProperty(nameof(Term), "Term")]
public partial class CoursesViewModel : ObservableObject
{
    [ObservableProperty]
    public ObservableCollection<Course> courses;

    [ObservableProperty]
    private Term currentTerm;

    private readonly IViewCollectionUseCase<Course> viewCoursesUseCase;

    public CoursesViewModel(IViewCollectionUseCase<Course> viewCoursesUseCase)
    {
        this.viewCoursesUseCase = viewCoursesUseCase;
        this.Courses = new ObservableCollection<Course>();

    }

    // Clearing the observable collection and then readding them wasn't seeming to update the UI
    // when editing a course, but assigning Course to a new observable collection does...
    public async Task LoadCoursesAsync(int termId)
    {
        var coursesList = await viewCoursesUseCase.ExecuteAsync(termId);

        if (coursesList != null)
        {
            Courses = new ObservableCollection<Course>(coursesList);
        }
    }

    [RelayCommand]
    public async Task GotoAddCourse()
    {
        var termId = this.CurrentTerm?.TermId ?? 0; // Get the current TermId

        await Shell.Current.GoToAsync($"{nameof(AddCoursePage)}?TermId={termId}");
    }

    [RelayCommand]
    public async Task GotoCourseDetails(Course course)
    {
        var queryParamater = new Dictionary<string, object>
        {
            {"Course", course }
        };

        await Shell.Current.GoToAsync(nameof(CourseDetailPage), true, queryParamater);
    }
}
