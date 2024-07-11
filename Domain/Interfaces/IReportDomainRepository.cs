using Infrastructure.Interfaces;
using Infrastructure.Model;

namespace Domain.Interfaces;

public interface IReportDomainRepository : IInfrastructureRepository<Report>
{
    Task<Report> AddAsync(Report report);
    Task<Report?> UpdateAsync(long id,Report report);
    Task<Report?> DeleteAsync(Report report);
    
    Task<Report?> GetReportByTypeAsync(string type);
    bool ReportExistsByImgByCertifiedNumberAndCylinderNumberAndEmitDateAndVehicleIdentifier(string certifiedNumber, string cylinderNumber, DateTime emitDate, string vehicleIdentifier);
}