using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiaryApi.DataAccess;
using DiaryApi.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DiaryApi
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

            //Data Access Context
            services.AddDbContext<DiaryAccessContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Dependency Injection
            services.AddTransient<IRepo, Repository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2); //Not required option
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseCors("CorsPolicy"); //Using CORS Policy for default settings
            app.UseDefaultFiles();
            app.UseMvc(); //Not required option

            //Auto Database Migration when running database container for the first time
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetService<DiaryAccessContext>().MigrateDB();
            }
        }
    }
}
