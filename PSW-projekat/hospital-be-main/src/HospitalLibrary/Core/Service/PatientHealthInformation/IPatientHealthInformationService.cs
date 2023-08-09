using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Service
{
    public interface IPatientHealthInformationService : IService<PatientHealthInformation>
    {
        PatientHealthInformation GetLatestByPatientId(int id);
    }
}
