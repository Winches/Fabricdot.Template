﻿using System;
using System.Collections.Generic;
using System.IO;
using Fabricdot.WebApi.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ProjectName.WebApi.Configuration;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ProjectName.WebApi.Configuration
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjectName API", Version = "v1" });
                c.DocumentFilter<LowercaseDocumentFilter>();
                c.SchemaFilter<EnumerationSchemaFilter>();
                c.EnableAnnotations();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization:'Bearer token'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
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

                var xmlFile = new[] { "ProjectName.WebApi", "ProjectName.Domain", "ProjectName.Domain.Shared" };
                c.IncludeXmlComments(xmlFile, true);
            });
            return services;
        }

        public static IApplicationBuilder UserSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();

            // swagger endpoint.
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1"));
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
}