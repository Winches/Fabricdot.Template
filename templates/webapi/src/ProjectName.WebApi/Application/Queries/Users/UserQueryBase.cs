using System;

namespace ProjectName.WebApi.Application.Queries.Users
{
    public abstract class UserQueryBase
    {
        public Guid UserId { get; }

        protected UserQueryBase(Guid userId)
        {
            UserId = userId;
        }
    }
}