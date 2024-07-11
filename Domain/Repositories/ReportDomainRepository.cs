using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure.Model;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories;

public class ReportDomainRepository(AppDbContext context) : InfrastructureRepository<Report>(context), IReportDomainRepository
{
    public async Task<Report> AddAsync(Report report)
    {
        await context.Set<Report>().AddAsync(report);
        await context.SaveChangesAsync();
        return report;
    }

    public async Task<Report?> UpdateAsync(long id,Report report)
    {
        var reportToUpdate= await context.Set<Report>().FindAsync(id);
        if (reportToUpdate == null) throw new Exception("Report not found");
        context.Set<Report>().Update(reportToUpdate);
        await context.SaveChangesAsync();
        return reportToUpdate;
    }

    public async Task<Report?> DeleteAsync(Report report)
    {
        context.Set<Report>().Remove(report);
        await context.SaveChangesAsync();
        return report;
    }

    public async Task<Report?> GetReportByTypeAsync(string type)
    {
        return await context.Set<Report>().Where(report => report.Type == type).FirstOrDefaultAsync();
    }

    public async Task<string?> GetReportImgByCertifiedNumberAndCylinderNumberAndEmitDateAndVehicleIdentifier(string certifiedNumber,
        string cylinderNumber, DateTime emitDate, string vehicleIdentifier)
    {
        return await context.Set<Report>().Where(report => report.CertificationNumber == certifiedNumber &&
                                                           report.CylinderNumber == cylinderNumber &&
                                                           report.EmitDate.Date.Year == emitDate.Date.Year &&
                                                           report.EmitDate.Date.Month == emitDate.Date.Month &&
                                                           report.EmitDate.Date.Day == emitDate.Date.Day &&
                                                           report.VehicleIdentifier == vehicleIdentifier)
            .Select(report => report.CylinderNumber).FirstOrDefaultAsync();
    }
}