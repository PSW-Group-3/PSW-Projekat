﻿using IntegrationLibrary.Core.Model;
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
