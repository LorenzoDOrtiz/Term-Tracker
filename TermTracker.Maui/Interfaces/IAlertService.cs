namespace TermTracker.Maui.Interfaces;
public interface IAlertService
{
    Task ShowToast(string text);
    Task ScheduleLocalNotification(DateTime alertTypeDate, int alertId, string title, string description, double notifyDays);
    public bool CancelLocalNotification(int alertId);
}
