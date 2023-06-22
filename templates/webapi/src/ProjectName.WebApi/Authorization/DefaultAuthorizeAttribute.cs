using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace ProjectName.WebApi.Authorization;

public class DefaultAuthorizeAttribute : AuthorizeAttribute
{
    public DefaultAuthorizeAttribute()
    {
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
    }
}
