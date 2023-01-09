using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Repository.EmergencyBloodRequests
{
    public class EmergencyBloodRequestRepository : IEmergencyBloodRequestRepository
    {
        private readonly IntegrationDbContext _context;
        public EmergencyBloodRequestRepository(IntegrationDbContext context)
        {
            _context = context;
        }

        public void Create(EmergencyBloodRequest entity)
        {
            _context.EmergencyBloodRequests.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(EmergencyBloodRequest entity)
        {
            _context.EmergencyBloodRequests.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<EmergencyBloodRequest> GetAll()
        {
            return _context.EmergencyBloodRequests.ToList();
        }

        public EmergencyBloodRequest GetById(int id)
        {
            return _context.EmergencyBloodRequests.Find(id);
        }

        public void Update(EmergencyBloodRequest entity)
        {
            throw new NotImplementedException();
        }
    }
}
