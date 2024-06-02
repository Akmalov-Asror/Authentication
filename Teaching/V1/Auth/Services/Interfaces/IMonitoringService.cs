using Teaching.Common.Data.Configurations;
using Teaching.V1.Auth.Models.MonitoringModels;

namespace Teaching.V1.Auth.Services.Interfaces;

public interface IMonitoringService
{
    ValueTask<MonitoringModel> Get(Guid id);
    ValueTask<PagedResult<MonitoringModel>> GetByFilter(MonitoringFilterModel filter);
    ValueTask<MonitoringModel> CreateMonitoring(MonitoringCreateModel model);
    ValueTask<MonitoringModel> Update(MonitoringUpdateModel model);
    ValueTask<bool> Delete(Guid id);
}
