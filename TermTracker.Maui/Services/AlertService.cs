using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Plugin.LocalNotification;
using TermTracker.Maui.Interfaces;

namespace TermTracker.Maui.Services;
public class AlertService : IAlertService
{
    public async Task ShowToast(string text)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        ToastDuration duration = ToastDuration.Long;
        double fontSize = 14;

        var toast = Toast.Make(text, duration, fontSize);

        await toast.Show(cancellationTokenSource.Token);
    }

    public async Task ScheduleLocalNotification(DateTime alertTypeDate, int alertId, string title, string description, double notifySeconds)
    {
        if (await LocalNotificationCenter.Current.AreNotificationsEnabled() == false)
        {
            await LocalNotificationCenter.Current.RequestNotificationPermission();
        }

        // Determine the target notify time as a DateTime
        DateTime targetNotifyTime = alertTypeDate.AddSeconds(-notifySeconds);

        var notification = new NotificationRequest
        {
            NotificationId = alertId,
            Title = title,
            Description = description,
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime = targetNotifyTime.ToLocalTime()
            }
        };

        await LocalNotificationCenter.Current.Show(notification);
    }


    public bool CancelLocalNotification(int alertId)
    {
        var deletedBool = LocalNotificationCenter.Current.Cancel(alertId);
        return deletedBool;
    }
}
