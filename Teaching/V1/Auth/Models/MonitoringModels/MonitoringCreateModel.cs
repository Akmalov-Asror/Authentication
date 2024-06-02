using System.ComponentModel.DataAnnotations;

namespace Teaching.V1.Auth.Models.MonitoringModels;

public class MonitoringCreateModel
{
    [Required]
    public string ProjectName { get; set; }
    [Required]
    public string PrivatePartner { get; set; }
    [Required]
    public int Timeofperiod { get; set; }
    [Required]
    public DateTime RegistryNumberAndDate { get; set; }
    [Required]
    public DateTime SubmissionAndAcceptanceDate { get; set; }
    [Required]
    public decimal ProjectValue { get; set; }
    [Required]
    public decimal PrivatePartnerInvestment { get; set; }
    [Required]
    public decimal OperatingCosts { get; set; }
}
