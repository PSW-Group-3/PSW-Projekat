using HospitalLibrary.Core.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalLibrary.Core.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {

        private readonly HospitalDbContext _context;

        public AppointmentRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Appointment> GetAll()
        {
            return _context.Appointments.ToList();          
        }

        public IEnumerable<Appointment> GetAllByDoctor(int personId)
        {
            return _context.Appointments.Include(x => x.Doctor).Include(x => x.Patient)
                .Where(x => x.Doctor.Person.Id == personId && !x.Deleted).ToList();
        }

        public IEnumerable<DateTime> GetAllFreeByDoctor(int doctorId,DateTime start,DateTime end)
        {
            var pom = _context.Appointments.Include(x => x.Patient).Include(x => x.Doctor)
                .Where(a => a.Patient == null && a.DateTime.Date >=start.Date && a.CancelationDate <= end.Date).Select(a => a.DateTime);
         
            var pom1= _context.Appointments.Include(x => x.Patient).Include(x => x.Doctor)
                .Where(a => a.Patient != null && a.DateTime >= start.Date && a.CancelationDate <= end.Date &&  a.Doctor.Id == doctorId).Select(a => a.DateTime);
            return pom.Except(pom1);
            return _context.Appointments.Include(x => x.Patient).Include(x => x.Doctor)
                .Where(a => a.Patient == null).Except(_context.Appointments.Include(x => x.Patient).Include(x => x.Doctor)
                .Where(a => a.Patient != null && a.Doctor.Id == doctorId)).Select(a => a.DateTime).ToList();
            /*var param1Value = new SqlParameter("id", doctorId);
            var result =  _context.Appointments.FromSqlRaw("select * from dbo.Appointments where PatientId is" +
                " null except select * from dbo.Appointments where PatientId is not null and DoctorId = @id",
               param1Value);
            return result.Select(p => p.DateTime).ToList();*/
        }


        public IEnumerable<DateTime> GetAllFree(ICollection<Doctor> doctors, DateTime start, DateTime end)
        {
            var p = this.GetAllFreeByDoctor(doctors.First().Id,start,end);

            foreach (Doctor doc in doctors) {
                var pom1 = this.GetAllFreeByDoctor(doc.Id, start, end);
                p = p.Intersect(pom1);
            }
           
            return p.ToList();
           
        }

        public Appointment GetById(int id)
        {
            return _context.Appointments.Find(id);
        }

        public void Create(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
        }

        public void Update(Appointment room)
        {
            _context.Entry(room).State = EntityState.Modified;

            try
            {
                _context.Update(room);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }


        public void Delete(Appointment room)
        {
            _context.Appointments.Remove(room);
            _context.SaveChanges();
        }

        public IEnumerable<Appointment> GetAllForPatient(int patientId)
        {
            return _context.Appointments.Where(p => p.Patient.Id == patientId).ToList();
        }

        public IEnumerable<Patient> GetAllMaliciousPatients()
        {
            return _context.Appointments.FromSqlRaw("SELECT TOP (1000) PatientId FROM[HospitalDb].[dbo].[Appointments] WHERE CancelationDate > DATEADD(day, -30, GETDATE()) GROUP BY PatientId HAVING COUNT(*) > 2").Select(a => a.Patient).ToList();
            //return (_context.Appointments.Where(a => a.DateTime > System.DateTime.Now.AddDays(-30)).GroupBy(a => a.Patient.Id)).Where(g => g.Count() > 2).Select(g => g.First().Patient).ToArray();
        }

        public IEnumerable<Appointment> GetAllByDoctorInDateRange(
            int doctorId,
            DateTime fromDate,
            DateTime toDate
        )
        {
            return _context.Appointments.Include(x => x.Doctor).Include(x => x.Patient)
                .Where(x => x.Doctor.Id == doctorId && !x.Deleted && x.CancelationDate == null && fromDate <= x.DateTime && x.DateTime < toDate).ToList();
        }

        public IEnumerable<Appointment> GetAllByPatientInDateRange(
            int patientId,
            DateTime fromDate,
            DateTime toDate
        )
        {
            return _context.Appointments.Include(x => x.Doctor).Include(x => x.Patient)
                .Where(x => x.Patient.Id == patientId && !x.Deleted && fromDate <= x.DateTime && x.DateTime < toDate).ToList();
        }

        public IEnumerable<Appointment> GetAllForDoctorByDate(int doctorId, DateTime scheduledDate)
        {
            return _context.Appointments.Where(a => a.Doctor.Id == doctorId && a.DateTime.Date == scheduledDate.Date).ToList();
        }

        public bool CheckIfExists(Appointment appointment)
        {
            IEnumerable<Appointment> a = _context.Appointments.Include(x => x.Doctor).Include(x => x.Patient)
                .Where(x => x.Doctor.Id == appointment.Doctor.Id && !x.Deleted && x.CancelationDate == null && x.DateTime == appointment.DateTime).ToList();
            return a.Count() == 0 ? false : true;
        }
    }
}
