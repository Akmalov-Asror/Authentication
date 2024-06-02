using Teaching.Common.Entities.Users;

namespace Teaching.V1.Auth.Models.MonitoringModels;

public class MonitoringModel
{
    public Guid Id { get; set; }
    public string ProjectName { get; set; }
    public string PrivatePartner { get; set; }
    public int? Timeofperiod { get; set; }
    public DateTime RegistryNumberAndDate { get; set; }
    public DateTime SubmissionAndAcceptanceDate { get; set; }
    public decimal? ProjectValue { get; set; }
    public decimal? PrivatePartnerInvestment { get; set; }
    public decimal? OperatingCosts { get; set; }
    public MonitoringModel MapFromEntity(Monitoring entity)
    {
        Id = entity.Id;
        ProjectName = entity.ProjectName;
        PrivatePartner = entity.PrivatePartner;
        Timeofperiod = entity.Timeofperiod;
        RegistryNumberAndDate = entity.RegistryNumberAndDate;
        SubmissionAndAcceptanceDate = entity.SubmissionAndAcceptanceDate;
        ProjectValue = entity.ProjectValue;
        PrivatePartnerInvestment = entity.PrivatePartnerInvestment;
        OperatingCosts = entity.OperatingCosts;
        return this;
    }
    public MonitoringModel MapFromEntityies(Monitoring entity)
    {
        return this;
    }
}
