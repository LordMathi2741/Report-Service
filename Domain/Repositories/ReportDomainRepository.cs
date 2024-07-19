using System.Collections;
using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Infrastructure.Model;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

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

    public async Task<Report?> GetReportByVehicleIdentifier(string vehicleIdentifier)
    {
        return await context.Set<Report>().Where(report => report.VehicleIdentifier == vehicleIdentifier)
            .FirstOrDefaultAsync();
    }

    public async Task<Report?> GetReportByCylinderNumber(string cylinderNumber)
    {
        return await context.Set<Report>().Where(report => report.CylinderNumber == cylinderNumber)
            .FirstOrDefaultAsync();
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

    public async Task<Hashtable> GetTotalReportsByBrandByYearAsync(string brand, int year)
    {
        var reports = await context.Set<Report>()
            .Where(report => report.Brand == brand && report.EmitDate.Year == year)
            .ToListAsync();

        Hashtable result = new Hashtable();

        foreach (var report in reports)
        {
            var month = report.EmitDate.Month;
            var monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);

            if (result.ContainsKey(monthName))
            {
                result[monthName] = (int)result[monthName] + 1;
            }
            else
            {
                result.Add(monthName, 1);
            }
        }

        return result;
    }

    public async Task<Hashtable> GetTotalReportsByOperationCenterByYearAndMonthAsync( int year,int month)
    {
        var reports = await context.Set<Report>()
            .Where(report =>  report.EmitDate.Year == year && report.EmitDate.Month == month)
            .ToListAsync();

        Hashtable result = new Hashtable();

        foreach (var report in reports)
        {
            var operationCenter = report.OperationCenter;
            if (result.ContainsKey(operationCenter))
            {
                result[operationCenter] = (int)result[operationCenter] + 1;
            }
            else
            {
                result.Add(operationCenter, 1);
            }
        }

        return result;
    }

    public async Task<Hashtable> CountReportsTypeByYearAndMonthAsync(int year, int month)
    {
        var reports = await context.Set<Report>()
            .Where(report => report.EmitDate.Year == year && report.EmitDate.Month == month)
            .ToListAsync();

        Hashtable result = new Hashtable();

        foreach (var report in reports)
        {
            var type = report.Type;
            if (result.ContainsKey(type))
            {
                result[type] = (int)result[type] + 1;
            }
            else
            {
                result.Add(type, 1);
            }
        }

        return result;
    }
}