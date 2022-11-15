using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalLibrary.Core.Model;

namespace HospitalLibrary.Core.Repository
{
    public interface IWorkingDayRepository : IRepository<WorkingDay>
    {
        IEnumerable<WorkingDay> GetAllWorkingDaysByUser(int personId);

    }
}
