using HospitalLibrary.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.AggregatDoctor
{
    public class DoctorExaminationEventsRepository
    {
        public HospitalDbContext _context;

        public DoctorExaminationEventsRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public DoctorExamination Create(DoctorExamination examination)
        {
            examination.startTime = DateTime.Now;
            _context.DoctorExaminations.Add(examination);
            _context.SaveChanges();

            return examination;
        }

        public DoctorExamination findById(int id)
        {
            return _context.DoctorExaminations.Find(id);
        }

        public void AddExaminationTimeEvent(DoctorExamination examination)
        {
            _context.DoctorExaminationEvents.Add(examination.Changes.Last());
            _context.SaveChanges();
        }
    }
}
