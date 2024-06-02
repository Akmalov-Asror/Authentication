using Teaching.Common.Entities.Users;
using Teaching.V1.Auth.Models.MonitoringModels;

namespace Teaching.Common.UnitOfWork.Interfaces;

public interface IMonitoringRepository
{
    Task<Monitoring> GetMonitoringByIdAsync(Guid id);
    Task<List<Monitoring>> GetAllMonitoringsAsync(bool includeMonitory);
    Task<Monitoring> AddMonitoringAsync(Monitoring monitoring);
    Task<int> GetCount(MonitoringFilterModel model);
    Task<List<Monitoring>> GetByFilter(MonitoringFilterModel model, string[] includes = null);
    Task<Monitoring> UpdateMonitoringAsync(Monitoring monitoring);
    Task<Monitoring> DeleteMonitoringAsync(Guid id);
}
