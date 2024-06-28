using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TermTracker.CoreBusiness.Models;
using TermTracker.Maui.Interfaces;
using TermTracker.Maui.Views;
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
    private readonly IDeleteUseCase<Course> deleteCourseUseCase;
    private readonly IAlertService alertService;

    public CourseViewModel(IViewUseCase<Course> viewCourseUseCase,
                           IAddUseCase<Course> addCourseUseCase,
                           IEditUseCase<Course> editCourseUseCase,
                           IDeleteUseCase<Course> deleteCourseUseCase,
                           IAlertService alertService)
    {
        this.addCourseUseCase = addCourseUseCase;
        this.editCourseUseCase = editCourseUseCase;
        this.deleteCourseUseCase = deleteCourseUseCase;
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
    public async Task GotoCourseEdit(int courseId)
    {
        await Shell.Current.GoToAsync($"{nameof(EditCoursePage)}?Id={courseId}");
    }

    [RelayCommand]
    public async Task GotoCourseAlerts(int courseId)
    {
        await Shell.Current.GoToAsync($"{nameof(CourseAlertPage)}");
    }

    [RelayCommand]
    public async Task GotoCourseNotes()
    {
        await Shell.Current.GoToAsync($"{nameof(CourseNotesPage)}");
    }

    [RelayCommand]
    public void AddCourseStartDateAlert(int courseId)
    {
        int id = 0;

        if (Course.StartDateAlerts.Any())
        {
            var maxId = Course.StartDateAlerts.Max(x => x.Id);
            id = maxId + 1;
        }

        var newAlert = new StartDateAlert { CourseId = courseId, Id = id };
        Course.StartDateAlerts.Add(newAlert);
    }

    [RelayCommand]
    public void AddCourseEndDateAlert(int courseId)
    {
        int id = 1000;

        if (Course.EndDateAlerts.Any())
        {
            var maxId = Course.EndDateAlerts.Max(x => x.Id);
            id = maxId + 1;
        }

        var newAlert = new EndDateAlert { CourseId = courseId, Id = id };
        Course.EndDateAlerts.Add(newAlert);
    }

    [RelayCommand]
    public async Task UpdateAlertsAndScheduleNotifications()
    {
        foreach (var alert in Course.StartDateAlerts)
        {
            await UpdateAlert(alert);
            await ScheduleNotification(alert);
        }

        foreach (var alert in Course.EndDateAlerts)
        {
            await UpdateAlert(alert);
            await ScheduleNotification(alert);
        }
        await alertService.ShowToast("Alerts Saved.");
    }

    private async Task UpdateAlert(Alert alert)
    {
        if (alert is StartDateAlert)
        {
            var existingAlert = Course.StartDateAlerts.FirstOrDefault(a => a.Id == alert.Id);
            if (existingAlert != null)
            {
                existingAlert.ReminderValue = alert.ReminderValue;
                existingAlert.ReminderUnit = alert.ReminderUnit;
            }
        }
        else if (alert is EndDateAlert)
        {
            var existingAlert = Course.EndDateAlerts.FirstOrDefault(a => a.Id == alert.Id);
            if (existingAlert != null)
            {
                existingAlert.ReminderValue = alert.ReminderValue;
                existingAlert.ReminderUnit = alert.ReminderUnit;
            }
        }
    }

    private async Task ScheduleNotification(Alert alert)
    {
        string title = "Alert Notification";

        // Determine notifyDays based on alert.ReminderUnit
        double notifySeconds = CalculateNotifySeconds(alert);

        if (alert is StartDateAlert)
        {
            string description = $"{Course.Name} Starts in: {alert.ReminderValue} {alert.ReminderUnit}";

            await alertService.ScheduleLocalNotification(Course.StartDate, alert.Id, title, description, notifySeconds);

        }

        if (alert is EndDateAlert)
        {
            string description = $"{Course.Name} Ends in: {alert.ReminderValue} {alert.ReminderUnit}";

            await alertService.ScheduleLocalNotification(Course.EndDate, alert.Id, title, description, notifySeconds);

        }
    }

    private double CalculateNotifySeconds(Alert alert)
    {
        // Convert alert.ReminderUnit to appropriate seconds
        switch (alert.ReminderUnit)
        {
            case "seconds":
                return alert.ReminderValue;
            case "minutes":
                return alert.ReminderValue * 60;
            case "hours":
                return alert.ReminderValue * 60 * 60;
            case "days":
                return alert.ReminderValue * 24 * 60 * 60;
            case "weeks":
                return alert.ReminderValue * 7 * 24 * 60 * 60;
            default:
                return 0;
        }
    }

    [RelayCommand]
    public void DeleteAlert(Alert alert)
    {
        var alertId = alert.Id;

        if (alert is StartDateAlert)
        {
            var existingAlert = Course.StartDateAlerts.FirstOrDefault(a => a.Id == alert.Id);
            Course.StartDateAlerts.Remove(existingAlert);
            alertService.CancelLocalNotification(alertId);
        }
        if (alert is EndDateAlert)
        {
            var existingAlert = Course.EndDateAlerts.FirstOrDefault(a => a.Id == alert.Id);
            Course.EndDateAlerts.Remove(existingAlert);
            alertService.CancelLocalNotification(alertId);
        }
    }

    [RelayCommand]
    public async Task UpdateNotes(Course course)
    {
        if (course != null)
        {
            await editCourseUseCase.ExecuteAsync(course.Id, course);
            await alertService.ShowToast("Course notes saved.");
        }
    }

    [RelayCommand]
    public async Task ShareNotes(Course course)
    {
        if (course != null)
        {
            await Share.Default.RequestAsync(new ShareTextRequest
            {
                Text = course.Notes,
                Title = "Share Course Notes",
                Subject = $"{course.Name} Notes"
            });
        }
    }

    [RelayCommand]
    public async Task DeleteCourse(int courseId)
    {
        var termId = this.Course.TermId;

        bool answer = await Application.Current.MainPage.DisplayAlert("Delete Confirmation", "Are you sure you want to delete this course?", "Delete", "Cancel");

        if (answer)
        {
            await deleteCourseUseCase.ExecuteAsync(courseId);
            await Shell.Current.GoToAsync("..");
        }
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

    [RelayCommand]
    public async Task AddAssessment()
    {
        var result = await Application.Current.MainPage.DisplayActionSheet("Choose Assessment Type", "Cancel", null, "Objective Assessment", "Performance Assessment");

        if (result == "Objective Assessment")
        {
            var newAssessment = new ObjectiveAssessment { Name = "Objective Assessment" };
            Course.Assessments.Add(newAssessment);
        }
        else if (result == "Performance Assessment")
        {
            var newAssessment = new PerformanceAssessment { Name = "Performance Assessment" };
            Course.Assessments.Add(newAssessment);
        }
    }
}
