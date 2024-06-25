using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TermTracker.CoreBusiness.Models;
using TermTracker.Maui.Interfaces;
using TermTracker.UseCases.UseCaseInterfaces;

namespace TermTracker.Maui.ViewModels;
public partial class CourseViewModel : ObservableObject
{
    [ObservableProperty]
    private Course course;

    private readonly IAddUseCase<Course> addCourseUseCase;
    private readonly IViewUseCase<Course> viewCourseUseCase;
    private readonly IEditUseCase<Course> editCourseUseCase;
    private readonly IAlertService alertService;


    public CourseViewModel(IViewUseCase<Course> viewCourseUseCase,
                            IAddUseCase<Course> addCourseUseCase,
                           IEditUseCase<Course> editCourseUseCase,
                           IAlertService alertService)
    {
        this.addCourseUseCase = addCourseUseCase;
        this.editCourseUseCase = editCourseUseCase;
        this.viewCourseUseCase = viewCourseUseCase;
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
    public async Task LoadCourse(int courseId)
    {
        this.Course = await this.viewCourseUseCase.ExecuteAsync(courseId);
    }

    [RelayCommand]
    public async Task EditCourse()
    {
        if (!await IsValidCourse())
        {
            return;
        }
        await this.editCourseUseCase.ExecuteAsync(this.Course.CourseId, this.Course);



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
