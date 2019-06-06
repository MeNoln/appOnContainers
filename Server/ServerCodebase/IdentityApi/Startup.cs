using IdentityApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace IdentityApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(); //Cross Origin Resource Sharing for talking between the containers
            services.AddScoped<IAuthService, AuthService>(); //Dependency Injection
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2); //Mvc framework

            //Healthchecks for API
            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            //Filtes the set of HealthChecks executed
            app.UseHealthChecks("/hc", new HealthCheckOptions()
            {
                Predicate = r => r.Name.Contains("self")
            });

            app.UseCors("CorsPolicy");
            app.UseDefaultFiles();
            app.UseMvc();
        }
    }
}
