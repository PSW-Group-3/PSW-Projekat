﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;

namespace HospitalLibrary.Core.Service
{
    public interface IRoomService : IService<Room>
    {
        IEnumerable<BedDto> GetAllBedsByRoom(int roomId);

    }
}
