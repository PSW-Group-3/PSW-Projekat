using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationLibrary.Core.Repository.BloodRequests
{
    public class BloodRequestRepository : IBloodRequestRepository
    {
        private readonly IntegrationDbContext _context;

        public BloodRequestRepository(IntegrationDbContext context)
        {
            _context = context;
        }

        public void Create(BloodRequest entity)
        {
            _context.BloodRequests.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(BloodRequest entity)
        {
            _context.BloodRequests.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<BloodRequest> GetAll()
        {
            return _context.BloodRequests.ToList();
        }

        public IEnumerable<BloodRequest> GetAllByType(BloodType bloodType)
        {
            return _context.BloodRequests
                .Where(x =>  x.BloodType == bloodType && x.RequestState == RequestState.Accepted).ToList();
        }


        public BloodRequest GetById(int id)
        {
            return _context.BloodRequests.Find(id); 
        }

        public void Update(BloodRequest entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity.BloodQuantity).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
