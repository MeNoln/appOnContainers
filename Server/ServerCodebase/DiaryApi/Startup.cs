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
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.HealthChecks;

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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2); //MVC Pattern

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info { Title = "SwaggerUI", Version = "v1" }); }); //Swagger

            //Sql HealthChecks
            services.AddHealthChecks(checks => 
            {
                checks.WithDefaultCacheDuration(TimeSpan.FromSeconds(1));
                checks.AddSqlCheck("DefaultConnection", Configuration.GetConnectionString("DefaultConnection"));
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseCors("CorsPolicy"); //Using CORS Policy for default settings
            app.UseDefaultFiles();
            app.UseMvc(); //Default settings

            //Setting Swagger up for testing Api`s from browser
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "DiaryApi - v1"); });

            //Auto Database Migration when running database container for the first time
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetService<DiaryAccessContext>().MigrateDB();
            }
        }
    }
}
