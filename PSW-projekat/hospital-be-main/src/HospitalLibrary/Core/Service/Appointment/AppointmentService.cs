using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.DTOs.CreatingAppointmentsDTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IWorkingDayRepository _workingDayRepository;
        private readonly IPatientRepository _patientRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository, IWorkingDayRepository workingDayRepository, IDoctorRepository doctorRepository, IPatientRepository patientRepository)
        {
            _appointmentRepository = appointmentRepository;
            _workingDayRepository = workingDayRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
        }

        public bool InWorkingTime(Appointment entity, IEnumerable<WorkingDay> workingDays)
        {

            foreach (WorkingDay workingDay in workingDays)
            {
                DayOfWeek dayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), workingDay.Day.ToString());

                DateTime wStart = new DateTime(1, 1, 1, workingDay.StartTime.Hour, workingDay.StartTime.Minute, workingDay.StartTime.Second);
                DateTime wEnd = new DateTime(1, 1, 1, workingDay.EndTime.Hour, workingDay.EndTime.Minute, workingDay.EndTime.Second);
                DateTime aTime = new DateTime(1, 1, 1, entity.DateTime.Hour, entity.DateTime.Minute, entity.DateTime.Second);

                if ((dayOfWeek.Equals(entity.DateTime.DayOfWeek)) && wStart <= aTime && wEnd >= aTime)
                {
                    return true;
                }
            }
            return false;
        }

        public void Create(Appointment entity)
        {
            /*
             if (InWorkingTime(entity, workingDayRepository.GetAllWorkingDaysByUser(3)))
             {
                 entity.Deleted = false;
                 _appointmentRepository.Create(entity);
             }
             */
            entity.CancelationDate = null;
            entity.Deleted = false;
            _appointmentRepository.Create(entity);
        }

        public void Delete(Appointment entity)
        {
            try
            {
                SentEmail(entity);

                _appointmentRepository.Delete(entity);
            }
            catch (Exception e) { }
        }

        public void SentEmail(Appointment appointment)
        {
            string fromMail = "hospitalpswisa@gmail.com";
            string fromPassword = "uleoarfabsegnuxa";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Termin za pregled";
            message.To.Add(appointment.Patient.Person.Email.Adress.ToString());
            message.Body = "<html><body> Vas termin: " + appointment.DateTime.ToString() + " za pregled je obrisan.</body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);
        }

        public IEnumerable<Appointment> GetAll()
        {
            return _appointmentRepository.GetAll();
        }

        public Appointment GetById(int id)
        {
            return _appointmentRepository.GetById(id);
        }

        public void Update(Appointment entity)
        {
            
           // if (InWorkingTime(entity, workingDayRepository.GetAllWorkingDaysByUser(2)))
            {
                entity.Deleted = false;
                _appointmentRepository.Update(entity);
            }

        }

        public IEnumerable<AppointmentDto> GetAllByDoctor(int personId)
        {
            IEnumerable<Appointment> allAppointments = _appointmentRepository.GetAllByDoctor(personId);
            List<AppointmentDto> appointmentsDtos = new();

            foreach (Appointment appointment in allAppointments)
            {

                AppointmentDto appointmentDto = new AppointmentDto();


                PatientDto patientDto = new PatientDto();
                patientDto.Id = appointment.Patient.Id;
                patientDto.Name = appointment.Patient.Person.Name;
                patientDto.Surname = appointment.Patient.Person.Surname;

                DoctorDto doctorDto = new DoctorDto();
                doctorDto.Id = appointment.Doctor.Id;
                doctorDto.Name = appointment.Doctor.Person.Name;
                doctorDto.Surname = appointment.Doctor.Person.Surname;

                appointmentDto.Patient = patientDto;
                appointmentDto.Doctor = doctorDto;
                appointmentDto.DateTime = appointment.DateTime;

                appointmentDto.Id = appointment.Id;

                appointmentsDtos.Add(appointmentDto);
            }
            return appointmentsDtos;
            
        }

        public IEnumerable<Appointment> GetAllAppointmentsForPatient(int patientId)
        {
            return _appointmentRepository.GetAllForPatient(patientId);
        }

        public IEnumerable<Patient> GetAllMaliciousPatients()
        {
            return _appointmentRepository.GetAllMaliciousPatients();
        }

        public List<AppointmentsAvailableForCreatingAppointment> GetAllAvailableAppointmentsForCreatingAppointment(
            CheckAvailableAppontmentDto checkAvailableAppontment
        )
        {
            DateTime fromDate;
            DateTime toDate;
            DateTime fromTime;
            DateTime toTime;

            if (!DateTime.TryParse(checkAvailableAppontment.fromDate, out fromDate)) return null;
            if (!DateTime.TryParse(checkAvailableAppontment.toDate, out toDate)) return null;
            if (!DateTime.TryParse(checkAvailableAppontment.fromTime, out fromTime)) return null;
            if (!DateTime.TryParse(checkAvailableAppontment.toTime, out toTime)) return null;

            if (!(fromDate < toDate)) return null;
            if (!(fromTime < toTime)) return null;

            // Clamp time
            if (fromTime.Second % 60 != 0) fromTime = fromTime.AddSeconds(-(fromTime.Second % 60));
            if (fromTime.Minute % 20 != 0) fromTime = fromTime.AddMinutes(-(fromTime.Minute % 20));
            if (toTime.Second % 60 != 0) toTime = toTime.AddSeconds(60 - toTime.Second % 60);
            if (toTime.Minute % 20 != 0) toTime = toTime.AddMinutes(20 - toTime.Minute % 20);

            if (DateTime.Parse(fromDate.ToShortDateString()+" "+fromTime.ToShortTimeString()) < DateTime.Now) return null;

            string prefer;
            if (!(checkAvailableAppontment.prefer == "doctor" || checkAvailableAppontment.prefer == "time")) return null;
            else prefer = checkAvailableAppontment.prefer;

            if (_doctorRepository.getPersonByDoctorId(checkAvailableAppontment.selectedDoctorID) == null) return null;
            if (_patientRepository.getPatientByPersonId(checkAvailableAppontment.personID) == null) return null;

            int doctorID = checkAvailableAppontment.selectedDoctorID;
            int patientID = _patientRepository.getPatientByPersonId(checkAvailableAppontment.personID).Id;

            IEnumerable<Appointment> allDoctorAppointments = _appointmentRepository.GetAllByDoctorInDateRange(doctorID, fromDate, toDate);
            IEnumerable<Appointment> allPatientAppointments = _appointmentRepository.GetAllByPatientInDateRange(patientID, fromDate, toDate);

            Doctor doctor = _doctorRepository.GetById(doctorID);
            Patient patient = _patientRepository.GetById(patientID);

            IEnumerable<WorkingDay> workingDays = _workingDayRepository.GetAllWorkingDaysByUser(doctor.Person.Id);

            List<AppointmentsAvailableForCreatingAppointment> availableAppointments = new List<AppointmentsAvailableForCreatingAppointment>();

            //IMPLEMENTIRATI LOGIKU

            //currCheckDate
            for (DateTime currCheckDate = fromDate; currCheckDate < toDate; currCheckDate = currCheckDate.AddDays(1))
            {
                //currDayInWeek
                foreach (WorkingDay workingDay in workingDays)
                {
                    if ((int)workingDay.Day == (int)currCheckDate.DayOfWeek)
                    {
                        //currCheckTime
                        for (DateTime currCheckTime = fromTime; currCheckTime < toTime; currCheckTime = currCheckTime.AddMinutes(20))
                        {
                            if (workingDay.StartTime.TimeOfDay <= currCheckTime.TimeOfDay &&
                                workingDay.EndTime.TimeOfDay > currCheckTime.TimeOfDay)
                            {
                                DateTime currCheck = DateTime.Parse(currCheckDate.ToShortDateString() + " " + currCheckTime.ToShortTimeString());
                                bool patientIsFree = true;
                                bool doctorIsFree = true;

                                //Patient free?
                                foreach (Appointment aPatient in allPatientAppointments)
                                {
                                    //Patient has appointment in that time
                                    if (aPatient.DateTime.Equals(currCheck))
                                    {
                                        patientIsFree = false;
                                        break;
                                    };
                                    patientIsFree = true;
                                }

                                //Doctor free?
                                foreach (Appointment aDoctor in allDoctorAppointments)
                                {
                                    //Doctor has appointment in that time
                                    if (aDoctor.DateTime.Equals(currCheck))
                                    {
                                        doctorIsFree = false;
                                        break;
                                    };
                                    doctorIsFree = true;
                                }

                                //Both are free
                                if (patientIsFree && doctorIsFree)
                                    availableAppointments.Add(new AppointmentsAvailableForCreatingAppointment(
                                        currCheck.DayOfWeek.ToString(),
                                        currCheck.ToShortDateString(),
                                        currCheck.ToShortTimeString(),
                                        doctor.Person.Name + " " + doctor.Person.Surname,
                                        doctor.Specialization.ToString(),
                                        doctor.Id
                                    ));
                            }
                        }
                    }
                }
            }

            if (availableAppointments.Count == 0)
                if (prefer.Equals("time"))
                {
                    //Time is priority
                    IEnumerable<Doctor> similarDoctors = _doctorRepository.GetAllBySpecialization(doctor.Specialization);

                    foreach (Doctor currDoctor in similarDoctors)
                    {
                        allDoctorAppointments = _appointmentRepository.GetAllByDoctorInDateRange(currDoctor.Id, fromDate, toDate);
                        workingDays = _workingDayRepository.GetAllWorkingDaysByUser(currDoctor.Person.Id);
                        //currCheckDate
                        for (DateTime currCheckDate = fromDate; currCheckDate < toDate; currCheckDate = currCheckDate.AddDays(1))
                        {
                            //currDayInWeek
                            foreach (WorkingDay workingDay in workingDays)
                            {
                                if ((int)workingDay.Day == (int)currCheckDate.DayOfWeek)
                                {
                                    //currCheckTime
                                    for (DateTime currCheckTime = fromTime; currCheckTime < toTime; currCheckTime = currCheckTime.AddMinutes(20))
                                    {
                                        if (workingDay.StartTime.TimeOfDay <= currCheckTime.TimeOfDay &&
                                            workingDay.EndTime.TimeOfDay > currCheckTime.TimeOfDay)
                                        {
                                            DateTime currCheck = DateTime.Parse(currCheckDate.ToShortDateString() + " " + currCheckTime.ToShortTimeString());
                                            bool patientIsFree = true;
                                            bool doctorIsFree = true;

                                            //Patient free?
                                            foreach (Appointment aPatient in allPatientAppointments)
                                            {
                                                //Patient has appointment in that time
                                                if (aPatient.DateTime.Equals(currCheck))
                                                {
                                                    patientIsFree = false;
                                                    break;
                                                };
                                                patientIsFree = true;
                                            }

                                            //Doctor free?
                                            foreach (Appointment aDoctor in allDoctorAppointments)
                                            {
                                                //Doctor has appointment in that time
                                                if (aDoctor.DateTime.Equals(currCheck))
                                                {
                                                    doctorIsFree = false;
                                                    break;
                                                };
                                                doctorIsFree = true;
                                            }

                                            //Both are free
                                            if (patientIsFree && doctorIsFree)
                                                availableAppointments.Add(new AppointmentsAvailableForCreatingAppointment(
                                                    currCheck.DayOfWeek.ToString(),
                                                    currCheck.ToShortDateString(),
                                                    currCheck.ToShortTimeString(),
                                                    currDoctor.Person.Name + " " + currDoctor.Person.Surname,
                                                    currDoctor.Specialization.ToString(),
                                                    currDoctor.Id
                                                ));
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
                else
                {
                    workingDays = _workingDayRepository.GetAllWorkingDaysByUser(doctor.Person.Id);
                    //New times
                    fromDate = fromDate.AddDays(-5);
                    toDate = toDate.AddDays(5);

                    //Doctor is priority
                    for (DateTime currCheckDate = fromDate; currCheckDate < toDate; currCheckDate = currCheckDate.AddDays(1))
                    {
                        //currDayInWeek
                        foreach (WorkingDay workingDay in workingDays)
                        {
                            if ((int)workingDay.Day == (int)currCheckDate.DayOfWeek)
                            {
                                //currCheckTime
                                for (DateTime currCheckTime = fromTime; currCheckTime < toTime; currCheckTime = currCheckTime.AddMinutes(20))
                                {
                                    if (workingDay.StartTime.TimeOfDay <= currCheckTime.TimeOfDay &&
                                        workingDay.EndTime.TimeOfDay > currCheckTime.TimeOfDay)
                                    {
                                        DateTime currCheck = DateTime.Parse(currCheckDate.ToShortDateString() + " " + currCheckTime.ToShortTimeString());
                                        bool patientIsFree = true;
                                        bool doctorIsFree = true;

                                        //Patient free?
                                        foreach (Appointment aPatient in allPatientAppointments)
                                        {
                                            //Patient has appointment in that time
                                            if (aPatient.DateTime.Equals(currCheck))
                                            {
                                                patientIsFree = false;
                                                break;
                                            };
                                            patientIsFree = true;
                                        }

                                        //Doctor free?
                                        foreach (Appointment aDoctor in allDoctorAppointments)
                                        {
                                            //Doctor has appointment in that time
                                            if (aDoctor.DateTime.Equals(currCheck))
                                            {
                                                doctorIsFree = false;
                                                break;
                                            };
                                            doctorIsFree = true;
                                        }

                                        //Both are free
                                        if (patientIsFree && doctorIsFree)
                                            availableAppointments.Add(new AppointmentsAvailableForCreatingAppointment(
                                                currCheck.DayOfWeek.ToString(),
                                                currCheck.ToShortDateString(),
                                                currCheck.ToShortTimeString(),
                                                doctor.Person.Name + " " + doctor.Person.Surname,
                                                doctor.Specialization.ToString(),
                                                doctor.Id
                                            ));
                                    }
                                }
                            }
                        }
                    }
                }

            return availableAppointments;
        }

        public void ScheduleAppointment(Appointment appointment)
        {
            _appointmentRepository.Create(appointment);
        }

        public List<string> GetFreeAppointmentsForDoctor(int doctorId, DateTime scheduledDate)
        {
            ICollection<DoctorSchedule> doctorSchedules = _doctorRepository.GetById(doctorId).DoctorSchedules;
            foreach (DoctorSchedule ds in doctorSchedules)
            {
                if ((int)ds.Day == (int)scheduledDate.DayOfWeek)
                {
                    List<string> allAppointmentTimes = new List<string>();
                    Time currentTime = ds.Shift.StartTime;
                    while (!(currentTime.ToString().Equals(ds.Shift.EndTime.ToString())))
                    {
                        allAppointmentTimes.Add(currentTime.ToString());
                        currentTime = currentTime.AddMinutes(20);
                    }

                    List<Appointment> scheduledAppointments = (List<Appointment>)_appointmentRepository.GetAllForDoctorByDate(doctorId, scheduledDate);
                    List<string> scheduledAppointmentTimes = new List<string>();
                    foreach (Appointment appointment in scheduledAppointments)
                    {
                        scheduledAppointmentTimes.Add(appointment.DateTime.TimeOfDay.ToString().Substring(0, 5));
                    }

                    return allAppointmentTimes.Except(scheduledAppointmentTimes).ToList();
                }
            }
            return null;
        }

        public bool CreateCustomAppointment(CustomAppointmentForCreatingDto checkAppointment)
        {
            Appointment appointment = new Appointment();

            Patient patient = _patientRepository.getPatientByPersonId(int.Parse(checkAppointment.PersonID));
            if (patient == null) return false;
            appointment.Patient = patient;

            Doctor doctor = _doctorRepository.GetById(int.Parse(checkAppointment.DoctorID));
            if (doctor == null) return false;
            appointment.Doctor = doctor;

            DateTime dateTime;
            if (!(DateTime.TryParse(checkAppointment.CreateDate, out dateTime))) return false;
            if (dateTime.Second % 60 != 0) dateTime = dateTime.AddSeconds(60 - dateTime.Second % 60);
            if (dateTime.Minute % 20 != 0) dateTime = dateTime.AddMinutes(20 - dateTime.Minute % 20);
            if (dateTime < DateTime.Now) return false;
            appointment.DateTime = dateTime;

            appointment.CancelationDate = null;
            appointment.Deleted = false;

            if (!_appointmentRepository.CheckIfExists(appointment))
            {
                _appointmentRepository.Create(appointment);
                return true;
            }
            return false;
        }

        public async Task<bool> CheckIfPatientHadAppointmentInPastXMonths(int patientId, int months)
        {
            return await _appointmentRepository.CheckIfPatientHadAppointmentInPastXMonths(patientId, months);
        }
    }
}
