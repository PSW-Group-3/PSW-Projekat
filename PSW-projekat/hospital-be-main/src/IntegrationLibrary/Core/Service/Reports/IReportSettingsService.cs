using IntegrationLibrary.Core.Model;
using System.Collections.Generic;

namespace IntegrationLibrary.Core.Service.Reports
{
    public interface IReportSettingsService
    {
        IEnumerable<ReportSettings> GetAll();
        ReportSettings GetFirst();
        void Create(ReportSettings entity);
        void Update(ReportSettings entity);
        void Delete(ReportSettings entity);

        ReportSettings GetById(int id);
        
    }
}
