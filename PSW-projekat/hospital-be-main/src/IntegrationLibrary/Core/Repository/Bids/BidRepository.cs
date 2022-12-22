using IntegrationLibrary.Core.Model.Tender;
using IntegrationLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Repository.Bids
{
    public class BidRepository :IBidRepository
    {
        private readonly IntegrationDbContext _context;
        public BidRepository(IntegrationDbContext context) 
        {
            _context = context;
        }

        public void Create(Bid entity)
        {
            _context.Bids.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Bid entity)
        {
            _context.Bids.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Bid> GetAll()
        {
            return _context.Bids.ToList();
        }

        public Bid GetById(int id)
        {
            return _context.Bids.Find(id);
        }

        public IEnumerable<Bid> GetByTenderId(int id)
        {
            return (from bids in _context.Bids
                    where bids.TenderOfBidId == id
                    select bids).ToList();
        }

        public IEnumerable<Bid> getFromDateToDate(DateTime start, DateTime end)
        {
            return (from bids in _context.Bids
                    where bids.Status == BidStatus.WIN && bids.DeliveryDate > start && bids.DeliveryDate < end
                    select bids).ToList();
        }

        public void Update(Bid entity)
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
