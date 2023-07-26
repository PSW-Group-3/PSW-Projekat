using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationLibrary.Core.Repository.Reports
{
    public class ReportSettingsRepository : IReportSettingsRepository
    {
        private readonly IntegrationDbContext _context;

        public ReportSettingsRepository(IntegrationDbContext context)
        {
            _context = context;
        }

        public void Create(ReportSettings entity)
        {
            _context.ReportSettings.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(ReportSettings entity)
        {
            _context.ReportSettings.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<ReportSettings> GetAll()
        {
            return _context.ReportSettings.ToList();
        }

        public ReportSettings GetById(int id)
        {
            return _context.ReportSettings.Find(id);
        }

        public ReportSettings GetFirst()
        {
            return _context.ReportSettings.FirstOrDefault();
        }

        public void Update(ReportSettings entity)
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
