using System;
using Integration.HouseManagement;
using Integration.HouseManagementService;
using Integration.HouseManagementServiceAsync;
using SISHaU.Library.API;

namespace ServiceHelperHost.Library.GisServiceModel.HouseManagement
{
    public class ImportNotificationDataModel : Requester<HouseManagementPortsTypeClient, HouseManagementPortsTypeAsyncClient>
    {
        public importNotificationRequest CreateRequest(string topic, string content, bool isImportant, bool isShipOff)
        {
            var request = GenerateGenericType <importNotificationRequest> ();
            request.notification = new[] {CreateRequestNotification(topic, content, isImportant, isShipOff)};
            return request;
        }

        public importNotificationRequest CreateRequest(string notificationGuid)
        {
            var request = GenerateGenericType<importNotificationRequest>();
            request.notification = new[] { CreateRequestRemoveNotification(notificationGuid) };
            return request;
        }

        /// <summary>
        /// Создание, редактирование новости в ГИС ЖКХ
        /// </summary>
        /// <param name="topic">Тема</param>
        /// <param name="content">Текст новости</param>
        /// <param name="isImportant">Высокая важность новости</param>
        /// <param name="isShipOff">Направить новость адресатам</param>
        /// <returns></returns>
        private importNotificationRequestNotification CreateRequestNotification(string topic, string content, bool isImportant = false, bool isShipOff = false)
        {
            var notification = new importNotificationRequestNotification
            {

                TransportGUID = Guid.NewGuid().ToString(),
                Create = new importNotificationRequestNotificationCreate
                {
                    Topic = topic,
                    /*IsShipOffSpecified = false,
                    IsImportantSpecified = false,*/
                    content = content,
                    IsAll = true,
                    IsNotLimit = true
                }
                //NotificationGUID = Guid.NewGuid().ToString(),
            };

            if (null == notification.Create) return notification;

            if (isShipOff)
            {
                notification.Create.IsShipOff = true;
            }

            if (isImportant)
            {
                notification.Create.IsImportant = true;
            }

            //Items = new object[]{ new Dictionary<string, bool> {{"IsAll" , true}} },
            /*
                "FIASHouseGuid", typeof(string)
                "IsAll", typeof(bool)
            */

            //item.Items = new[] { fiasHouseGuid ?? true };


            return notification;
        }

        private importNotificationRequestNotification CreateRequestRemoveNotification(string notificationGuid)
        {
            var notification = new importNotificationRequestNotification
            {
                TransportGUID = TransportGuid,
                NotificationGUID = notificationGuid,
                DeleteNotification = new DeleteDocType
                {
                    Delete = true
                }
            };

            return notification;
        }
    }
}
