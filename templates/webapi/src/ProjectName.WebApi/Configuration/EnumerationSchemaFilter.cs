using Fabricdot.Domain.ValueObjects;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ProjectName.WebApi.Configuration
{
    public class EnumerationSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsAssignableTo(typeof(Enumeration)))
            {
                schema.Type = "integer";
                schema.Format = "int32";
            }
        }
    }
}