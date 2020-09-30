using Fabricdot.Infrastructure.EntityFrameworkCore;

namespace Fabricdot.Infrastructure.Data
{
    public class EntityChangeTracker : EfEntityChangeTracker
    {
        /// <inheritdoc />
        public EntityChangeTracker(AppDbContext context) : base(context)
        {
        }
    }
}