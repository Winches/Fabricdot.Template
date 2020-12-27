using System;
using Fabricdot.Infrastructure.EntityFrameworkCore;

namespace FabricdotApp.Infrastructure.Data
{
    public class UnitOfWork : UnitOfWorkBase
    {
        public UnitOfWork(
            // ReSharper disable once SuggestBaseTypeForParameter
            AppDbContext context,
            IServiceProvider provider) : base(
                context,
                provider)
        {
        }
    }
}