using Infrastructure.Interfaces;
using Infrastructure.Model;

namespace Domain.Interfaces;

public interface IReportImgDomainRepository : IInfrastructureRepository<ReportImg>
{
    Task<ReportImg> AddReportImgAsync(ReportImg reportImg);
    Task<string?> GetReportImgByFileName(string filename);
}