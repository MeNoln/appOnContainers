using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace OcelotGateway
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
            services.AddOcelot(Configuration); //API Gateway
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2); //MVC Pattern

            //HealthChecks for Gateway
            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy());
        }

        public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            //Filtes the set of HealthChecks executed
            app.UseHealthChecks("/hc", new HealthCheckOptions()
            {
                Predicate = r => r.Name.Contains("self")
            });

            //Settings for access from Client side
            app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:5000").AllowAnyOrigin());
            await app.UseOcelot();
            app.UseMvc(); //Default settings
        }
    }
}
