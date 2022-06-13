﻿using System;
using System.Collections.Generic;
using Fabricdot.Infrastructure.Queries;

namespace ProjectName.WebApi.Application.Queries.Permissions
{
    /// <summary>
    ///     List all permissions of user
    /// </summary>
    public class GetUserPermissionsQuery : IQuery<ICollection<string>>
    {
        public Guid UserId { get; }

        public GetUserPermissionsQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}