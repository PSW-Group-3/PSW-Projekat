using HospitalLibrary.Core.Model.Enums;
using HospitalLibrary.Core.Repository.Notification;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Service.Notification
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        
        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        public void Create(Model.Notification entity)
        {
            _notificationRepository.Create(entity);
        }

        public void Delete(Model.Notification entity)
        {
            _notificationRepository.Delete(entity);
        }

        public IEnumerable<Model.Notification> GetAll()
        {
            return _notificationRepository.GetAll();
        }

        public IEnumerable<Model.Notification> GetAllByRole(Role role)
        {
            List<Model.Notification> notifications = new List<Model.Notification>();
            foreach (Model.Notification notification in _notificationRepository.GetAll())
            {
                if (notification.NotificationFor == role)
                    notifications.Add(notification);
            }
            return notifications;
        }

        public Model.Notification GetById(int id)
        {
            return _notificationRepository.GetById(id);
        }

        public void Update(Model.Notification entity)
        {
            _notificationRepository.Update(entity);
        }
    }
}
