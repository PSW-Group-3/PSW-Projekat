using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;
using HospitalLibrary.Core.Repository;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Service
{
    public class DietService : IDietService
    {
        private readonly IDietRepository _dietRepository;

        public DietService(IDietRepository dietRepository)
        {
            _dietRepository = dietRepository;
        }

        public void Create(DailyDiet entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(DailyDiet entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DailyDiet> GetAll()
        {
            throw new NotImplementedException();
        }

        public DailyDiet GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(DailyDiet entity)
        {
            throw new NotImplementedException();
        }
    }
}
