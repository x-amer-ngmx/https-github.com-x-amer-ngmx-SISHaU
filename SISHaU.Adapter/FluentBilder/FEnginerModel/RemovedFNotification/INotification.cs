using Integration;

namespace SISHaU.Adapter.FluentBilder.FEnginerModel.FNotification
{
    public interface INotification
    {
        T ImportNotificationsOfOrderExecution<T>(object param);
        T ImportNotificationsOfOrderExecutionCancellation<T>(object param);
        T ImportNotificationData<T>(object param);
        T ExportNotificationsOfOrderExecution<T>(object param);
    }
}
