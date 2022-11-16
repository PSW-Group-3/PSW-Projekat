using HospitalLibrary.Core.Model;
using HospitalLibrary.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Repository
{
    public class AllergyRepository : IRepository<Allergy>
    {
        private readonly HospitalDbContext _context;

        public AllergyRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public void Create(Allergy entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Allergy entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Allergy> GetAll()
        {
            return _context.Allergies.ToList();
        }

        public Allergy GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Allergy entity)
        {
            throw new NotImplementedException();
        }
    }
}
