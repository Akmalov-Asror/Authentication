using Teaching.Common.Data.Configurations;
using Teaching.Common.UnitOfWork.Interfaces;
using Teaching.V1.Auth.Models.MonitoringModels;
using Teaching.V1.Auth.Services.Exceptions;
using Teaching.V1.Auth.Services.Interfaces;

namespace Teaching.V1.Auth.Services.Monitoring
{
    public class MonitoringService : IMonitoringService
    {
        private readonly IMonitoringRepository _monitoringRepository;
        private readonly ILogger<MonitoringService> _logger;

        public MonitoringService(IMonitoringRepository monitoringRepository, ILogger<MonitoringService> logger)
        {
            _monitoringRepository = monitoringRepository;
            _logger = logger;
        }

        public async ValueTask<MonitoringModel> Get(Guid id)
        {
            return new MonitoringModel().MapFromEntity(await _monitoringRepository.GetMonitoringByIdAsync(id));
        }
        public async ValueTask<PagedResult<MonitoringModel>> GetByFilter(MonitoringFilterModel filter)
        {
            var count = await _monitoringRepository.GetCount(filter);
            var list = await _monitoringRepository.GetByFilter(filter);

            return PagedResult.Create(list.Select(x => new MonitoringModel().MapFromEntityies(x)).ToList(), filter.PageIndex, filter.PageSize, count);
        }

        public async ValueTask<MonitoringModel> CreateMonitoring(MonitoringCreateModel model)
        {
            try
            {
                var newMonitoring = new Common.Entities.Users.Monitoring()
                {
                    Id = Guid.NewGuid(),
                    ProjectName = model.ProjectName,
                    PrivatePartner = model.PrivatePartner,
                    Timeofperiod = model.Timeofperiod,
                    RegistryNumberAndDate = DateTime.Now,
                    SubmissionAndAcceptanceDate = DateTime.Now,
                    ProjectValue = model.ProjectValue,
                    PrivatePartnerInvestment = model.PrivatePartnerInvestment,
                    OperatingCosts = model.OperatingCosts
                };
                await _monitoringRepository.AddMonitoringAsync(newMonitoring);
                return new MonitoringModel().MapFromEntity(newMonitoring);
            }
            catch (Exception ex)
            {
                throw new UserException(400, $"monitoring_is_null_{ex.Message}");
            }
        }
        public async ValueTask<MonitoringModel> Update(MonitoringUpdateModel model)
        {
            try
            {
                var newMonitoring = new Common.Entities.Users.Monitoring()
                {
                    Id = model.Id,
                    ProjectName = model.ProjectName,
                    PrivatePartner = model.PrivatePartner,
                    Timeofperiod = model.Timeofperiod,
                    RegistryNumberAndDate = model.RegistryNumberAndDate,
                    SubmissionAndAcceptanceDate = model.SubmissionAndAcceptanceDate,
                    ProjectValue = model.ProjectValue,
                    PrivatePartnerInvestment = model.PrivatePartnerInvestment,
                    OperatingCosts = model.OperatingCosts
                };
                await _monitoringRepository.UpdateMonitoringAsync(newMonitoring);
                return new MonitoringModel().MapFromEntity(newMonitoring);
            }
            catch (UserException ex)
            {
                throw new UserException(400, $"monitoring_is_null_{ex.Message}");
            }
        }
        public async ValueTask<bool> Delete(Guid id)
        {
            await _monitoringRepository.DeleteMonitoringAsync(id);
            return true;
        }

    }
}
