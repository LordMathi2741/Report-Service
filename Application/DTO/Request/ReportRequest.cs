namespace Application.DTO.Request;

public class ReportRequest
{
    public string CertificationNumber { get; set; }
    public string CylinderNumber { get; set; }
    public string Brand { get; set; }
    public DateTime MadeDate { get; set; }
    public DateTime EmitDate { get; set; }
    public string Type { get; set; }
    public string VehicleIdentifier { get; set; }
    public string OperationCenter { get; set; }
    public long UserId { get; set; }
}