using HospitalLibrary.Core.Model;

namespace HospitalLibrary.Core.Repository
{
    public interface IPatientHealthInformationRepository : IRepository<PatientHealthInformation>
    {
        PatientHealthInformation GetLatestByPatientId(int id);
    }
}
