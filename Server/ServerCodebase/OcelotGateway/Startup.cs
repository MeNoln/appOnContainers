using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2); //Not required option
        }

        public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            //Settings for access from Client side
            app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:5000").AllowAnyOrigin());
            await app.UseOcelot();
            app.UseMvc();
        }
    }
}
