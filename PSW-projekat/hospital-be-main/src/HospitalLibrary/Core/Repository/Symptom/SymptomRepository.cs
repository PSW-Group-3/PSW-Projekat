using System;
using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Settings;

namespace HospitalLibrary.Core.Repository
{
    public class SymptomRepository : ISymptomRepository
    {
        private readonly HospitalDbContext _context;

        public SymptomRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public void Create(Symptom entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Symptom entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Symptom> GetAll()
        {
            return _context.Symptoms.ToList();
        }

        public Symptom GetById(int id)
        {
            return _context.Symptoms.Find(id);
        }

        public void Update(Symptom entity)
        {
            throw new NotImplementedException();
        }
    }
}
