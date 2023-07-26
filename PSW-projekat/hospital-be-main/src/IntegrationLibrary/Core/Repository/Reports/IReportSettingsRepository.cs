using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Repository.CRUD;

namespace IntegrationLibrary.Core.Repository.Reports
{
    public interface IReportSettingsRepository : ICRUDRepository<ReportSettings>
    {
        ReportSettings GetFirst();
    }
}
