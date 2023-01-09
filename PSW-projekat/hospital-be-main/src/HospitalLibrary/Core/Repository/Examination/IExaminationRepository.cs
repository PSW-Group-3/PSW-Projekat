using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalLibrary.Core.Model;

namespace HospitalLibrary.Core.Repository
{
    public interface IExaminationRepository : IRepository<Examination>
    {
        List<Examination> GetAllExaminationsByDoctor(int personId);

    }
}
