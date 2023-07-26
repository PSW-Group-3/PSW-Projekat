using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Repository.Reports;
using System.Collections.Generic;

namespace IntegrationLibrary.Core.Service.Reports
{
    public class ReportSettingsService : IReportSettingsService
    {
        private readonly IReportSettingsRepository _reportSettingsRepository;

        public ReportSettingsService(IReportSettingsRepository reportSettingsRepository)
        {
            _reportSettingsRepository = reportSettingsRepository;
        }

        public void Create(ReportSettings entity)
        {
            try
            {
                _reportSettingsRepository.Create(entity);
            }
            catch
            {

            }
        }

        public void Delete(ReportSettings entity)
        {
            try
            {
                _reportSettingsRepository.Delete(entity);
            }
            catch
            {

            }
        }

        public IEnumerable<ReportSettings> GetAll()
        {
            return _reportSettingsRepository.GetAll();
        }

        public ReportSettings GetById(int id)
        {
            return _reportSettingsRepository.GetById(id);
        }

        public ReportSettings GetFirst()
        {
            return _reportSettingsRepository.GetFirst();
        }

        public void Update(ReportSettings entity)
        {
            try
            {
                _reportSettingsRepository.Update(entity);
            }
            catch
            {

            }
        }
    }
}
