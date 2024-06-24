using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TermTracker.CoreBusiness.Models;
using TermTracker.Maui.Views;
using TermTracker.UseCases.Interfaces;

namespace TermTracker.Maui.ViewModels;

[QueryProperty(nameof(Term), "Term")]
public partial class CoursesViewModel : ObservableObject
{
    [ObservableProperty]
    public ObservableCollection<Course> courses;

    [ObservableProperty]
    private Term currentTerm; // Add this property

    private readonly IViewCoursesUseCase viewCoursesUseCase;

    public CoursesViewModel(IViewCoursesUseCase viewCoursesUseCase)
    {
        this.viewCoursesUseCase = viewCoursesUseCase;
        this.Courses = new ObservableCollection<Course>();

    }
    public async Task LoadCoursesAsync(int termId)
    {
        this.Courses.Clear();

        var coursesList = await viewCoursesUseCase.ExecuteAsync(termId);

        if (coursesList != null && coursesList.Count > 0)
        {
            foreach (var course in coursesList)
            {
                this.Courses.Add(course);
            }
        }
    }

    [RelayCommand]
    public async Task GotoAddCourse()
    {
        var termId = this.CurrentTerm?.TermId ?? 0; // Get the current TermId

        await Shell.Current.GoToAsync($"{nameof(AddCoursePage)}?TermId={termId}");
        Shell.Current.FlyoutIsPresented = false;
    }
}
