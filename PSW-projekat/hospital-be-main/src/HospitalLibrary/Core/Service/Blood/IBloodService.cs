﻿using HospitalLibrary.Core.DTOs;
using System.Collections.Generic;
﻿using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;

namespace HospitalLibrary.Core.Service
{
    public interface IBloodService : IService<Blood>
    {
        public void updateQuantityBlood(int bloodId, int quantity, Blood blood);
        public void handleBloodRequest(List<BloodOrderDto> order);

        public void updateEmergency(int quantity, BloodType bloodType);
        public bool StoreBlood(Blood blood);

    }
}
