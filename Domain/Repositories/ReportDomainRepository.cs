using System.Collections;
using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Infrastructure.Model;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories;

public class ReportDomainRepository(AppDbContext context, IUnitOfWork unitOfWork) : InfrastructureRepository<Report>(context), IReportDomainRepository
{
    public async Task<Report> AddAsync(Report report)
    {
        await context.Set<Report>().AddAsync(report);
        await unitOfWork.SaveChangesAsync();
        return report;
    }

    public async Task<Report?> UpdateAsync(long id,Report report)
    {
        var reportToUpdate= await context.Set<Report>().FindAsync(id);
        if (reportToUpdate == null) throw new Exception("Report not found");
        context.Set<Report>().Update(reportToUpdate);
        await unitOfWork.SaveChangesAsync();
        return reportToUpdate;
    }

    public async Task<Report?> DeleteAsync(Report report)
    {
        context.Set<Report>().Remove(report);
        await unitOfWork.SaveChangesAsync();
        return report;
    }

    public async Task<Report?> GetReportByTypeAsync(string type)
    {
        return await context.Set<Report>().Where(report => report.Type == type).FirstOrDefaultAsync();
    }

    public bool ReportExistsByImgByCertifiedNumberAndCylinderNumberAndEmitDateAndVehicleIdentifier(string certifiedNumber,
        string cylinderNumber, DateTime emitDate, string vehicleIdentifier)
    {
        return context.Set<Report>().Any(report => report.CertificationNumber == certifiedNumber &&
                                                         report.CylinderNumber == cylinderNumber &&
                                                         report.EmitDate.Date.Year == emitDate.Date.Year &&
                                                         report.EmitDate.Date.Month == emitDate.Date.Month &&
                                                         report.EmitDate.Date.Day == emitDate.Date.Day &&
                                                         report.VehicleIdentifier == vehicleIdentifier);
    }

    public async Task<Hashtable> GetTotalReportsByBrandByYearAsync(string brand)
    {
        var reports = await context.Set<Report>().Where(report => report.Brand == brand).ToListAsync();
        var reportsByYear = new Hashtable();
        foreach (var report in reports)
        {
            if (reportsByYear.ContainsKey(report.EmitDate.Year))
            {
                reportsByYear[report.EmitDate.Year] = (int) reportsByYear[report.EmitDate.Year] + 1;
            }
            else
            {
                reportsByYear.Add(report.EmitDate.Year, 1);
            }
        }

        return reportsByYear;
    }
}