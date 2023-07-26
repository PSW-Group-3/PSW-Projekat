using IntegrationLibrary.Core.Model.Tender;
using IntegrationLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationLibrary.Core.Repository.Tenders
{
    public class TenderRepository : ITenderRepository
    {
        private readonly IntegrationDbContext _context;

        public TenderRepository(IntegrationDbContext context)
        {
            _context = context;
        }

        public void Create(Tender entity)
        {
            _context.Tenders.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Tender entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tender> GetAll()
        {
            return _context.Tenders.ToList();
        }

        public Tender GetById(int id)
        {
            return _context.Tenders.Find(id);
        }
        public IEnumerable<Tender> GetAllOpen()
        {
            return (from tenders in _context.Tenders
                    where tenders.State == TenderState.OPEN
                    select tenders).ToList();
        }

        public void Update(Tender entity)
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

        public List<Tender> GetAllClosed()
        {
            return (from tenders in _context.Tenders
                    where tenders.State == TenderState.CLOSED
                    select tenders).ToList();
        }
    }
}
