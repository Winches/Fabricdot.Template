using Fabricdot.WebApi.Swagger;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ProjectName.WebApi.Configuration;

public static class SwaggerConfiguration
{
    private static readonly string[] s_setupAction = ["ProjectName.WebApi", "ProjectName.Domain", "ProjectName.Domain.Shared"];

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "ProjectName API",
                Description = "An ASP.NET Core Web API for managing ProjectName items",
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://opensource.org/license/mit")
                }
            });

            c.SchemaFilter<EnumerationSchemaFilter>();
            c.EnableAnnotations();
            c.DescribeAllParametersInCamelCase();
            c.IgnoreObsoleteProperties();
            c.IgnoreObsoleteActions();
            c.SupportNonNullableReferenceTypes();
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization:'Bearer token'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            }
            });

            var xmlFile = s_setupAction;
            c.IncludeXmlComments(xmlFile, true);
        });
        return services;
    }

    public static IApplicationBuilder UseSwagger(this IApplicationBuilder app)
    {
        SwaggerBuilderExtensions.UseSwagger(app);

        // swagger endpoint.
        app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "API V1"));
        return app;
    }

    public static void IncludeXmlComments(
    this SwaggerGenOptions options,
    string[] filenames,
    bool includeControllerXmlComments = false)
    {
        options.IncludeXmlComments(
            AppContext.BaseDirectory,
            filenames,
            includeControllerXmlComments);
    }

    public static void IncludeXmlComments(
        this SwaggerGenOptions options,
        string basePath,
        IEnumerable<string> filenames,
        bool includeControllerXmlComments = false)
    {
        filenames.ForEach(filename =>
        {
            var xml = filename.EndsWith(".xml") ? filename : $"{filename}.xml";
            var fullPath = Path.Combine(basePath, xml);
            if (File.Exists(fullPath))
                options.IncludeXmlComments(fullPath, includeControllerXmlComments);
        });
    }
}
