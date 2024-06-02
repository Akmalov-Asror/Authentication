namespace Teaching.Common.Entities.Users;

public class Monitoring : Auditable
{
    public string ProjectName { get; set; }
    public string PrivatePartner { get; set; }
    public int? Timeofperiod { get; set; }
    public DateTime RegistryNumberAndDate { get; set; }
    public DateTime SubmissionAndAcceptanceDate { get; set; }
    public decimal? ProjectValue { get; set; }
    public decimal? PrivatePartnerInvestment { get; set; }
    public decimal? OperatingCosts { get; set; }
}
