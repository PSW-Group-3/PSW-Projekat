﻿using HospitalLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Service.Notification
{
    public interface INotificationService : IService<Model.Notification>
    {
        IEnumerable<Model.Notification> GetAllByRole(Role role);
    }
}
