namespace TermTracker.Maui.Interfaces;
public interface IAlertService
{
    Task ShowToast(string text);
    Task ScheduleLocalNotification(int alertId, string title, string description, DateTime targetNotifyTime);
    public bool CancelLocalNotification(int alertId);
}
