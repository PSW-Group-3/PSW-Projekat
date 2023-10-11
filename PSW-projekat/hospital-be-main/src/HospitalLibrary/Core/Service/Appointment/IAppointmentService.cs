using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.DTOs.CreatingAppointmentsDTOs;
using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Service
{
    public interface IAppointmentService : IService<Appointment>
    {
        IEnumerable<AppointmentDto> GetAllByDoctor(int doctorId);
        void SentEmail(Appointment appointment);
        IEnumerable<Appointment> GetAllAppointmentsForPatient(int patientId);
        IEnumerable<Patient> GetAllMaliciousPatients();
        List<AppointmentsAvailableForCreatingAppointment> GetAllAvailableAppointmentsForCreatingAppointment
            (
                CheckAvailableAppontmentDto checkAvailableAppontment
            );
        void ScheduleAppointment(Appointment appointment);
        List<string> GetFreeAppointmentsForDoctor(int doctorId, DateTime scheduledDate);
        bool CreateCustomAppointment(CustomAppointmentForCreatingDto checkAppointment);
    }
}
