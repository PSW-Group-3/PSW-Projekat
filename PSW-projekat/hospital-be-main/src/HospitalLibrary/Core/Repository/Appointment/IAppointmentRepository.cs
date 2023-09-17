using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Repository
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        IEnumerable<Appointment> GetAllByDoctor(int doctorId);
        IEnumerable<DateTime> GetAllFreeByDoctor(int doctorId,DateTime start, DateTime end);
        IEnumerable<DateTime> GetAllFree(ICollection<Doctor> doctors, DateTime start, DateTime end);
        IEnumerable<Appointment> GetAllForPatient(int patientId);
        Task<bool> CheckIfPatientHadAppointmentInPastXMonths(int patientId, int months);

        IEnumerable<Patient> GetAllMaliciousPatients();
        IEnumerable<Appointment> GetAllByDoctorInDateRange(
            int doctorId,
            DateTime fromDate,
            DateTime toDate
        );
        IEnumerable<Appointment> GetAllByPatientInDateRange(
            int patientId,
            DateTime fromDate,
            DateTime toDate
        );
        IEnumerable<Appointment> GetAllForDoctorByDate(int doctorId, DateTime scheduledDate);
        bool CheckIfExists(Appointment appointment);
    }
}
