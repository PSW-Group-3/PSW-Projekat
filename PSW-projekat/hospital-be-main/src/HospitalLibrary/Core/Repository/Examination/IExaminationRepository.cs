using System.Collections.Generic;
using HospitalLibrary.Core.Model;

namespace HospitalLibrary.Core.Repository
{
    public interface IExaminationRepository : IRepository<Examination>
    {
        List<Examination> GetAllExaminationsByDoctor(int personId);

    }
}
