using HospitalLibrary.Core.Model;

namespace HospitalLibrary.Core.Service
{
    public interface IPatientHealthInformationService : IService<PatientHealthInformation>
    {
        PatientHealthInformation GetLatestByPatientId(int id);
    }
}
