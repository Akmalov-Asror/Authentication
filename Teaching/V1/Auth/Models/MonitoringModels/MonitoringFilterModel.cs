using Teaching.Common.Data.Configurations;

namespace Teaching.V1.Auth.Models.MonitoringModels;

public class MonitoringFilterModel : PaginationParams
{
    public Guid? Id { get; set; }
    public string? ProjectName { get; set; }
    public string? PrivatePartner { get; set; }
    public decimal? MinProjectValue { get; set; }
    public decimal? MaxProjectValue { get; set; }
}
