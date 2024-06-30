﻿using CommunityToolkit.Mvvm.ComponentModel;
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

    private Assessment _currentAssessment;
    public Assessment CurrentAssessment
    {
        get => _currentAssessment;
        set => SetProperty(ref _currentAssessment, value);
    }

    [ObservableProperty]
    private bool isEmailValid;

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
        int id = Course.StartDateAlerts.Count + 1;

        var newAlert = new StartDateAlert { CourseId = courseId, Id = id };
        Course.StartDateAlerts.Add(newAlert);
    }

    [RelayCommand]
    public void AddCourseEndDateAlert(int courseId)
    {
        int id = Course.EndDateAlerts.Count + 1;

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
            case "second(s)":
                return alert.ReminderValue;
            case "minute(s)":
                return alert.ReminderValue * 60;
            case "hour(s)":
                return alert.ReminderValue * 60 * 60;
            case "day(s)":
                return alert.ReminderValue * 24 * 60 * 60;
            case "week(s)":
                return alert.ReminderValue * 7 * 24 * 60 * 60;
            default:
                return 0;
        }
    }

    [RelayCommand]
    public void DeleteAlert(Alert alert)
    {
        if (alert is StartDateAlert)
        {
            var existingAlert = Course.StartDateAlerts.FirstOrDefault(a => a.Id == alert.Id);
            Course.StartDateAlerts.Remove(existingAlert);
            alertService.CancelLocalNotification(alert.Id);
        }
        if (alert is EndDateAlert)
        {
            var existingAlert = Course.EndDateAlerts.FirstOrDefault(a => a.Id == alert.Id);
            Course.EndDateAlerts.Remove(existingAlert);
            alertService.CancelLocalNotification(alert.Id);
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
            string message = "Course name cannot be empty.";
            await this.alertService.ShowToast(message);
            return false;
        }

        if (Course.StartDate.Date > Course.EndDate.Date)
        {
            string message = "Course start date cannot be after course end date.";
            await this.alertService.ShowToast(message);
            return false;
        }

        if (!IsEmailValid)
        {
            string message = "Invalid email address.";
            await this.alertService.ShowToast(message);
            return false;
        }

        return true;
    }

    [RelayCommand]
    public async Task AddAssessment()
    {
        var result = await Application.Current.MainPage.DisplayActionSheet("Choose Assessment Type", "Cancel", null, "Objective Assessment", "Performance Assessment");
        var id = Course.Assessments.Count + 1;

        if (result == "Objective Assessment")
        {
            var newAssessment = new ObjectiveAssessment { CourseId = Course.Id, Id = id, Name = "Objective Assessment" };
            Course.Assessments.Add(newAssessment);
        }
        else if (result == "Performance Assessment")
        {
            var newAssessment = new PerformanceAssessment { CourseId = Course.Id, Id = id, Name = "Performance Assessment" };
            Course.Assessments.Add(newAssessment);
        }
    }

    [RelayCommand]
    public void DeleteAssessment(Assessment assessment)
    {
        var existingAssessment = Course.Assessments.FirstOrDefault(a => a.Id == assessment.Id);
        Course.Assessments.Remove(existingAssessment);
    }

    [RelayCommand]
    public async Task GotoAssessmentAlertPage(Assessment assessment)
    {
        if (assessment != null)
        {
            CurrentAssessment = assessment;
            var assessmentType = assessment is ObjectiveAssessment ? "Objective" : "Performance";
            await Shell.Current.GoToAsync($"{nameof(AssessmentAlertPage)}?assessmentType={assessmentType}");
        }
    }


    [RelayCommand]
    public void AddAssessmentStartDateAlert()
    {
        var id = CurrentAssessment.StartDateAlerts.Count + CurrentAssessment.EndDateAlerts.Count + 1;
        var newAlert = new StartDateAlert { CourseId = CurrentAssessment.CourseId, Id = id };
        CurrentAssessment.StartDateAlerts.Add(newAlert);
    }

    [RelayCommand]
    public void AddAssessmentEndDateAlert()
    {
        var id = CurrentAssessment.StartDateAlerts.Count + CurrentAssessment.EndDateAlerts.Count + 1;
        var newAlert = new EndDateAlert { CourseId = CurrentAssessment.CourseId, Id = id };
        CurrentAssessment.EndDateAlerts.Add(newAlert);
    }

    [RelayCommand]
    public async Task UpdateAssessmentAlertsAndScheduleNotifications()
    {
        foreach (var alert in CurrentAssessment.StartDateAlerts)
        {
            await UpdateAssessmentAlert(alert);
            await ScheduleAssessmentNotification(alert);
        }

        foreach (var alert in CurrentAssessment.EndDateAlerts)
        {
            await UpdateAssessmentAlert(alert);
            await ScheduleAssessmentNotification(alert);
        }
        await alertService.ShowToast("Alerts Saved.");
    }

    private async Task UpdateAssessmentAlert(Alert alert)
    {
        if (alert is StartDateAlert)
        {
            var existingAlert = CurrentAssessment.StartDateAlerts.FirstOrDefault(a => a.Id == alert.Id);
            if (existingAlert != null)
            {
                existingAlert.ReminderValue = alert.ReminderValue;
                existingAlert.ReminderUnit = alert.ReminderUnit;
            }
        }
        else if (alert is EndDateAlert)
        {
            var existingAlert = CurrentAssessment.EndDateAlerts.FirstOrDefault(a => a.Id == alert.Id);
            if (existingAlert != null)
            {
                existingAlert.ReminderValue = alert.ReminderValue;
                existingAlert.ReminderUnit = alert.ReminderUnit;
            }
        }
    }

    private async Task ScheduleAssessmentNotification(Alert alert)
    {
        string title = "Alert Notification";

        // Determine notifyDays based on alert.ReminderUnit
        double notifySeconds = CalculateNotifySeconds(alert);

        if (alert is StartDateAlert)
        {
            string description = $"{Course.Name} {CurrentAssessment.Name} Starts in: {alert.ReminderValue} {alert.ReminderUnit}";

            await alertService.ScheduleLocalNotification(CurrentAssessment.StartDate, alert.Id, title, description, notifySeconds);
        }

        if (alert is EndDateAlert)
        {
            string description = $"{Course.Name} {CurrentAssessment.Name} Ends in: {alert.ReminderValue} {alert.ReminderUnit}";

            await alertService.ScheduleLocalNotification(CurrentAssessment.EndDate, alert.Id, title, description, notifySeconds);
        }
    }

    [RelayCommand]
    public void DeleteAssessmentAlert(Alert alert)
    {
        if (alert is StartDateAlert)
        {
            var existingAlert = CurrentAssessment.StartDateAlerts.FirstOrDefault(a => a.Id == alert.Id);
            CurrentAssessment.StartDateAlerts.Remove(existingAlert);
            alertService.CancelLocalNotification(alert.Id);
        }
        if (alert is EndDateAlert)
        {
            var existingAlert = CurrentAssessment.EndDateAlerts.FirstOrDefault(a => a.Id == alert.Id);
            CurrentAssessment.EndDateAlerts.Remove(existingAlert);
            alertService.CancelLocalNotification(alert.Id);
        }
    }
}
