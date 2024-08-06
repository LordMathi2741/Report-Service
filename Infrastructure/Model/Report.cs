using System.Runtime.CompilerServices;

namespace Infrastructure.Model;

/**
 *  Report model class
 * <p>
 *    - This class is used to represent a report.
 * </p>
 * <remarks>
 *    - Author : LordMathi2741
 *    - Version 1.0
 * </remarks>
 */
public partial class Report
{
    public long Id { get;  }
    public string CertificationNumber { get; set; }
    public string CylinderNumber { get; set; }
    
    public string CylinderType { get; set; }
    public string Brand { get; set; }
    public DateTime MadeDate { get; set; }
    public DateTime EmitDate { get; set; }
    
    
    public string Type { get; set; }
    public string VehicleIdentifier { get; set; }
    public string OperationCenter { get; set; }
    
    
    public long UserId { get; set; }
    
}



public partial class Report
{
    public Report()
    {
        CertificationNumber = string.Empty;
        CylinderNumber = string.Empty;
        CylinderType = string.Empty;
        Brand = string.Empty;
        MadeDate = DateTime.Now;
        EmitDate = DateTime.Now;
        Type = string.Empty;
        VehicleIdentifier = string.Empty;
        OperationCenter = string.Empty;
        
    }
    
    public Report(string certificationNumber, string cylinderNumber, string cylinderType, string brand, DateTime madeDate, DateTime emitDate, string type, string vehicleIdentifier, string operationCenter, long userId)
    {
        CertificationNumber = certificationNumber;
        CylinderNumber = cylinderNumber;
        CylinderType = cylinderType;
        Brand = brand;
        MadeDate = madeDate;
        EmitDate = emitDate;
        Type = type;
        VehicleIdentifier = vehicleIdentifier;
        OperationCenter = operationCenter;
        UserId = userId;
    }
}