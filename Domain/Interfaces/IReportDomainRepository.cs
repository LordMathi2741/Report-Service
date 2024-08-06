using System.Collections;
using Infrastructure.Interfaces;
using Infrastructure.Model;

namespace Domain.Interfaces;

public interface IReportDomainRepository : IInfrastructureRepository<Report>
{
    Task<Report> AddAsync(Report report);
    Task<Report?> UpdateAsync(long id,Report report);
    Task<Report?> DeleteAsync(Report report);
    
    Task<Report?> GetReportByTypeAsync(string type);

    Task<Report?> GetReportByVehicleIdentifier(string vehicleIdentifier);

    Task<Report?> GetReportByCylinderNumber(string cylinderNumber);
    bool ReportExistsByImgByCertifiedNumberAndCylinderNumberAndEmitDateAndVehicleIdentifier(string certifiedNumber, string cylinderNumber, DateTime emitDate, string vehicleIdentifier);
    Task<Hashtable> GetTotalReportsByBrandByYearAsync(string brand, int year);
    Task<Hashtable> GetTotalReportsByOperationCenterByYearAndMonthAsync( int year,int month);
    Task<Hashtable> CountReportsTypeByYearAndMonthAsync(int year, int month);
    
    Task<Hashtable> CountTotalReportsByYear(int year);
}