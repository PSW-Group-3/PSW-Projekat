using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public IEnumerable<BloodRequest> GetAllByType(HospitalLibrary.Core.Model.Enums.BloodType bloodType)
        {
            return _context.BloodRequests
                .Where(x =>  x.BloodType == getBloodType(bloodType) && x.RequestState == RequestState.Accepted).ToList();
        }

        private BloodType getBloodType(HospitalLibrary.Core.Model.Enums.BloodType type)
        {
            switch (type)
            {
                case HospitalLibrary.Core.Model.Enums.BloodType.BMinus:
                    return BloodType.BN;
                case HospitalLibrary.Core.Model.Enums.BloodType.AMinus:
                    return BloodType.AN;
                case HospitalLibrary.Core.Model.Enums.BloodType.ABMinus:
                    return BloodType.ABN;
                case HospitalLibrary.Core.Model.Enums.BloodType.OMinus:
                    return BloodType.ON;
                case HospitalLibrary.Core.Model.Enums.BloodType.BPlus:
                    return BloodType.BP;
                case HospitalLibrary.Core.Model.Enums.BloodType.APlus:
                    return BloodType.AP;
                case HospitalLibrary.Core.Model.Enums.BloodType.ABPlus:
                    return BloodType.ABP;
                case HospitalLibrary.Core.Model.Enums.BloodType.OPlus:
                    return BloodType.OP;
            }
            throw new Exception("Blood type isn't valid.");
        }

        public BloodRequest GetById(int id)
        {
            return _context.BloodRequests.Find(id); 
        }

        public void Update(BloodRequest entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
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
