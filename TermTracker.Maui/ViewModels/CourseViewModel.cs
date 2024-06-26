using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TermTracker.CoreBusiness.Models;
using TermTracker.Maui.Interfaces;
using TermTracker.UseCases.UseCaseInterfaces;

namespace TermTracker.Maui.ViewModels;
public partial class CourseViewModel : ObservableObject
{
    [ObservableProperty]
    private Course course;

    [ObservableProperty]
    private ObservableCollection<string> statuses;

    [ObservableProperty]
    private string selectedStatus;

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

        Statuses = new ObservableCollection<string>
            {
                "In Progress",
                "Completed",
                "Dropped",
                "Plan to Take"
            };
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
        await this.editCourseUseCase.ExecuteAsync(this.Course.Id, this.Course);



        await Shell.Current.GoToAsync("..");
    }

    public async Task<bool> IsValidCourse()
    {

        if (string.IsNullOrEmpty(this.Course.Name))
        {
            string message = "Course name can not be empty.";
            await this.alertService.ShowToast(message);
            return false;
        }

        if (Course.StartDate.Date > Course.EndDate.Date)
        {
            string message = "Course start date can not be after course end date.";
            await this.alertService.ShowToast(message);
            return false;
        }

        return true;
    }
}
