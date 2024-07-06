using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace Infrastructure.Model;

/**
 *  Auditable Report class
 *  <p>
 *   - This class is used to implement the audit fields for the Report entity.
 * </p>
 *  <remarks>
 *      - Author: LordMathi2741
 *      - Version : 1.0
 * </remarks>
 */
public partial class Report : IEntityWithCreatedUpdatedDate
{
    public DateTimeOffset? CreatedDate { get; set; }
    public DateTimeOffset? UpdatedDate { get; set; }
}