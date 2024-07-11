using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure.Model;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories;

public class ReportImgDomainRepository(AppDbContext context) : InfrastructureRepository<ReportImg>(context),IReportImgDomainRepository
{
    public async Task<ReportImg> AddReportImgAsync(ReportImg reportImg)
    {
        await context.Set<ReportImg>().AddAsync(reportImg);
        await context.SaveChangesAsync();
        return reportImg;
    }

    public async Task<string?> GetReportImgByFileName(string filename)
    {
        return await context.Set<ReportImg>().Where(rm => rm.FileName == filename).Select(rm => rm.Image).FirstOrDefaultAsync();
    }
}