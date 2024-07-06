using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure.Model;
using Infrastructure.Repositories;

namespace Domain.Repositories;

public class ReportDomainRepository(AppDbContext context) : InfrastructureRepository<Report>(context), IReportDomainRepository
{
    public async Task<Report> AddAsync(Report report)
    {
        await context.Set<Report>().AddAsync(report);
        await context.SaveChangesAsync();
        return report;
    }

    public async Task<Report?> UpdateAsync(Report report)
    {
        var reportToUpdate= await context.Set<Report>().FindAsync(report.Id);
        if (reportToUpdate == null) throw new Exception("Report not found");
        context.Set<Report>().Update(reportToUpdate);
        await context.SaveChangesAsync();
        return reportToUpdate;
    }

    public async Task<Report?> DeleteAsync(Report report)
    {
        var reportToDelete = await context.Set<Report>().FindAsync(report.Id);
        if (reportToDelete == null) throw new Exception("Report not found");
        context.Set<Report>().Remove(reportToDelete);
        await context.SaveChangesAsync();
        return reportToDelete;
    }
}