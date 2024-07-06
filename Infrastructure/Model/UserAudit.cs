using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace Infrastructure.Model;

/**
 * User entity with audit fields.
 * <p>
 *    - This is the user auditable entity.
 *    - It has the created and updated date fields.
 * </p>
 * <remarks>
 *     - Author: LordMathi2741
 *      - Version: 1.0
 * </remarks>
 */
public partial class User : IEntityWithCreatedUpdatedDate
{
    public DateTimeOffset? CreatedDate { get; set; }
    public DateTimeOffset? UpdatedDate { get; set; }
}