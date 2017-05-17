namespace SISHaU.Adapter.FluentBilder.FEnginerModel.FNotificationModel
{
    public interface INotification
    {
        void importNotificationsOfOrderExecution();
        void importNotificationsOfOrderExecutionCancellation();
        void importNotificationData();
        void exportNotificationsOfOrderExecution();
    }
}
