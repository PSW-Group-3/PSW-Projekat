using HospitalLibrary.Core.Model.Enums;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Service.Notification
{
    public interface INotificationService : IService<Model.Notification>
    {
        IEnumerable<Model.Notification> GetAllByRole(Role role);
    }
}
