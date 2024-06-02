using Microsoft.EntityFrameworkCore;
using Teaching.Common.Data;
using Teaching.Common.Entities.Users;
using Teaching.Common.UnitOfWork.Interfaces;
using Teaching.V1.Auth.Models.MonitoringModels;

namespace Teaching.Common.UnitOfWork.Implementation;

public class MonitoringRepository : IMonitoringRepository
{
    private readonly AppDbContext _appDbContext;

    public MonitoringRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Monitoring> AddMonitoringAsync(Monitoring monitoring)
    {
        var monitoryCreate = new Monitoring()
        {
            ProjectName = monitoring.ProjectName,
            PrivatePartner = monitoring.PrivatePartner,
            Timeofperiod = monitoring.Timeofperiod,
            RegistryNumberAndDate = DateTime.UtcNow,
            SubmissionAndAcceptanceDate = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Created = DateTime.UtcNow,
            ProjectValue = monitoring.ProjectValue,
            PrivatePartnerInvestment = monitoring.PrivatePartnerInvestment,
            OperatingCosts = monitoring.OperatingCosts
        };
        _appDbContext.Monitorings.Add(monitoryCreate);
        await _appDbContext.SaveChangesAsync();
        return monitoryCreate;
    }

    public async Task<Monitoring> DeleteMonitoringAsync(Guid id)
    {
        var deleteMonitoring = await _appDbContext.Monitorings.FirstOrDefaultAsync(m => m.Id == id);
        _appDbContext?.Monitorings.Remove(deleteMonitoring);
        await _appDbContext.SaveChangesAsync();
        return deleteMonitoring;
    }

    public async Task<List<Monitoring>> GetByFilter(MonitoringFilterModel model, string[] includes = null)
    {
        var query = GetQuery(model);

        if (includes is not null && includes.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        model.EnsureOrSetDefaults();
        query = query.Skip(model.PageSize * (model.PageIndex - 1)).Take(model.PageSize);

        return query.ToList();
    }

    //DRY- don't repeat yourself

    private IQueryable<Monitoring> GetQuery(MonitoringFilterModel model)
    {
        var query = _appDbContext.Monitorings.AsNoTracking();

        if (model.Id.HasValue && model.Id.Value != Guid.Empty)
        {
            query = query.Where(x => x.Id == model.Id.Value);
        }
        if (model.MinProjectValue.HasValue && model.MinProjectValue.Value > 0)
        {
            query = query.Where(x => x.ProjectValue >= model.MinProjectValue.Value);

        }
        if (model.MaxProjectValue.HasValue && model.MaxProjectValue > 0)
        {
            query = query.Where(x => x.ProjectValue <= model.MaxProjectValue.Value);
        }

        if (!string.IsNullOrEmpty(model.ProjectName) && !string.IsNullOrWhiteSpace(model.ProjectName))
        {
            query = query.Where(x => x.ProjectName.ToLower() == $"%{model.ProjectName.Trim().ToLower()}%");
        }


        if (!string.IsNullOrEmpty(model.PrivatePartner) && !string.IsNullOrWhiteSpace(model.PrivatePartner))
        {
            query = query.Where(x => x.PrivatePartner.ToLower() == $"%{model.PrivatePartner.Trim().ToLower()}%");
        }

        return query;
    }

    public async Task<int> GetCount(MonitoringFilterModel model)
    {
        var query = GetQuery(model);

        return await query.CountAsync();
    }

    public async Task<List<Monitoring>> GetAllMonitoringsAsync(bool includeMonitory)
    {
        return _appDbContext.Monitorings.ToList();
    }

    public async Task<Monitoring> GetMonitoringByIdAsync(Guid id)
    {
        return await _appDbContext.Monitorings.FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Monitoring> UpdateMonitoringAsync(Monitoring monitoring)
    {
        _appDbContext.Monitorings.Where(m => m.Id == monitoring.Id)
            .ExecuteUpdate(
            p => p.SetProperty(m => m.ProjectName, m => monitoring.ProjectName)
            .SetProperty(m => m.PrivatePartner, m => monitoring.PrivatePartner)
            .SetProperty(m => m.Updated, m => DateTime.UtcNow)
            .SetProperty(m => m.Created, m => monitoring.Created)
            .SetProperty(m => m.Timeofperiod, m => monitoring.Timeofperiod)
            .SetProperty(m => m.RegistryNumberAndDate, m => DateTime.UtcNow)
            .SetProperty(m => m.SubmissionAndAcceptanceDate, m => DateTime.UtcNow)
            .SetProperty(m => m.ProjectValue, m => monitoring.ProjectValue)
            .SetProperty(m => m.PrivatePartnerInvestment, m => monitoring.PrivatePartnerInvestment)
            .SetProperty(m => m.OperatingCosts, m => monitoring.OperatingCosts)
            );
        return await _appDbContext.Monitorings.AsNoTracking().FirstOrDefaultAsync(m => m.Id == monitoring.Id);

    }

    //1 get 
    //2 update 
    // 3 save 

}
