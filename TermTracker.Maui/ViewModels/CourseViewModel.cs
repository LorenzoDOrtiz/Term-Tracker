using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TermTracker.CoreBusiness.Models;
using TermTracker.Maui.Interfaces;
using TermTracker.UseCases.Interfaces;

namespace TermTracker.Maui.ViewModels;
public partial class CourseViewModel : ObservableObject
{
    [ObservableProperty]
    private Course course;

    private readonly IAddCourseUseCase addCourseUseCase;
    private readonly IAlertService alertService;
    public CourseViewModel(IAddCourseUseCase addCourseUseCase,
                         IAlertService alertService)
    {
        this.addCourseUseCase = addCourseUseCase;
        this.alertService = alertService;
    }

    [RelayCommand]
    public async Task AddCourse()
    {
        if (!await IsValidCourse())
        {
            return;
        }
        await this.addCourseUseCase.ExecuteAsync(this.Course);

        await Shell.Current.GoToAsync("..");
    }
    public async Task<bool> IsValidCourse()
    {

        if (string.IsNullOrEmpty(this.Course.CourseName))
        {
            string message = "Course name can not be empty.";
            await this.alertService.ShowToast(message);
            return false;
        }

        if (Course.CourseStartDate.Date > Course.CourseEndDate.Date)
        {
            string message = "Course start date can not be after course end date.";
            await this.alertService.ShowToast(message);
            return false;
        }

        return true;
    }
}
