using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Repository.Newses
{
    public class NewsRepository : INewsRepository
    {
        private readonly IntegrationDbContext _context;
        public NewsRepository()
        {
        }
        public NewsRepository(IntegrationDbContext context)
        {
            _context = context;
        }

        public void Create(News entity)
        {
            _context.Newses.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(News entity)
        {
            _context.Newses.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<News> GetAll()
        {
            return _context.Newses.ToList();
        }

        public News GetById(int id)
        {
            return _context.Newses.Find(id);
        }

        public void Update(News entity)
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

        public IEnumerable<News> GetAllPending()
        { 
            return (from newses in _context.Newses
                    where newses.Status == NewsStatus.PENDING
                    select newses).ToList();
                    
        }
    }
}
