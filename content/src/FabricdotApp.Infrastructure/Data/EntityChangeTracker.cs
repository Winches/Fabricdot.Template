using Fabricdot.Infrastructure.EntityFrameworkCore;

namespace FabricdotApp.Infrastructure.Data
{
    public class EntityChangeTracker : EfEntityChangeTracker
    {
        /// <inheritdoc />
        public EntityChangeTracker(AppDbContext context) : base(context)
        {
        }
    }
}