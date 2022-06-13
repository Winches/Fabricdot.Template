using System;
using System.Text.Json;
using System.Threading.Tasks;
using Fabricdot.Core.Boot;
using Fabricdot.Core.Modularity;
using Fabricdot.Domain.SharedKernel;
using Fabricdot.WebApi;
using Fabricdot.WebApi.Configuration;
using Fabricdot.WebApi.Tracing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectName.Infrastructure;
using ProjectName.WebApi.Configuration;

namespace ProjectName.WebApi
{
    [Requires(typeof(AppNameInfrastructureModule))]
    [Requires(typeof(FabricdotWebApiModule))]
    [Exports]
    public class AppNameApplicationModule : ModuleBase
    {
        public override void ConfigureServices(ConfigureServiceContext context)
        {
            var services = context.Services;

            #region endpoint

            services.AddControllers(opts => opts.AddActionFilters())
                    .ConfigureApiBehaviorOptions(opts => opts.SuppressModelStateInvalidFilter = true)
                    .AddJsonOptions(opts => opts.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter()));

            #endregion endpoint

            #region api-doc

            //swagger
            services.AddSwagger();

            #endregion api-doc

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

            app.UserSwagger();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            return Task.CompletedTask;
        }
    }
}