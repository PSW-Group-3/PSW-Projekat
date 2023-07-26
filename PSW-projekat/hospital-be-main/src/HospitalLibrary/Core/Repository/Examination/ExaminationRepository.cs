using HospitalLibrary.Core.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalLibrary.Core.Repository
{
    public class ExaminationRepository : IExaminationRepository
    {

        private readonly HospitalDbContext _context;
        public ExaminationRepository(HospitalDbContext context)
        {
            _context = context;
        }
        public void Create(Examination examination)
        {
            _context.Examinations.Add(examination);
            _context.SaveChanges();
        }

        public void Delete(Examination entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Examination> GetAll()
        {
            return _context.Examinations.ToList();
        }

        public Examination GetById(int id)
        {
            return _context.Examinations.Find(id);
        }

        public void Update(Examination entity)
        {
            throw new NotImplementedException();
        }

        public List<Examination> GetAllExaminationsByDoctor(int personId)
        {
            List<string> reports = new List<string>();
            IEnumerable<Examination> examinations = _context.Examinations.Include(x => x.Appointment.Doctor).Where(x => x.Appointment.Doctor.Person.Id == personId && !x.Deleted).ToList();
       
            List<Examination> result = (List<Examination>)examinations;
            return result;
        }

    }
}
