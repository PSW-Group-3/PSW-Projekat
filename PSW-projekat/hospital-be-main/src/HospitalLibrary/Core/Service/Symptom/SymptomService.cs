using System;
using System.Collections.Generic;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;

namespace HospitalLibrary.Core.Service
{
    public class SymptomService : ISymptomService
    {
        private readonly ISymptomRepository _symptomRepository;

        public SymptomService(ISymptomRepository symptomRepository)
        {
            _symptomRepository = symptomRepository;
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
            return _symptomRepository.GetAll();
        }

        public Symptom GetById(int id)
        {
            return _symptomRepository.GetById(id);
        }

        public void Update(Symptom entity)
        {
            throw new NotImplementedException();
        }
    }
}
