using Infrastructure.Interfaces;
using Infrastructure.Model;

namespace Domain.Interfaces;

public interface IReportDomainRepository : IInfrastructureRepository<Report>
{
    Task<Report> AddAsync(Report report);
    Task<Report?> UpdateAsync(Report report);
    Task<Report?> DeleteAsync(Report report);
}