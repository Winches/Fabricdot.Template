using System.Text.Json;
using Fabricdot.Core.Boot;
using Fabricdot.Core.Modularity;
using Fabricdot.Domain.SharedKernel;
using Fabricdot.WebApi;
using Fabricdot.WebApi.Tracing;
using Microsoft.AspNetCore.StaticFiles;
using ProjectName.Infrastructure;
using ProjectName.WebApi.Configuration;

namespace ProjectName.WebApi;

[Requires(typeof(AppNameInfrastructureModule))]
[Requires(typeof(FabricdotWebApiModule))]
[Exports]
public class AppNameApplicationModule : ModuleBase
{
    public override void ConfigureServices(ConfigureServiceContext context)
    {
        var services = context.Services;

        services.AddControllers()
                .AddJsonOptions(opts => opts.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter()));

        services.AddSwagger();

        SystemClock.Configure(DateTimeKind.Utc);
        services.AddSingleton<IContentTypeProvider, FileExtensionContentTypeProvider>();

        services.AddAuthenticationWithJwt(context.Configuration);
    }

    public override Task OnStartingAsync(ApplicationStartingContext context)
    {
        var services = context.ServiceProvider;
        var app = services.GetApplicationBuilder();
        var env = services.GetRequiredService<IWebHostEnvironment>();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseCorrelationId();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        if (env.IsDevelopment())
        {
            app.UseSwagger();
        }

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();

            if (env.IsDevelopment())
            {
                endpoints.MapGet("/", http =>
                {
                    http.Response.Redirect("swagger");
                    return Task.CompletedTask;
                });
            }
        });

        return Task.CompletedTask;
    }
}
