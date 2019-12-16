using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


using OpenTraumaRegistry.Data;

namespace OpenTraumaRegistry.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
 
            var dbProvider = Configuration.GetSection("TraumaRegistrySettings")["dbProvider"];
            var connectionString = Configuration.GetSection("TraumaRegistrySettings")["connectionString"];
           
            switch (dbProvider)
            {
                
                case "sqlserver":
                    services.AddDbContext<Context>(options => options.UseSqlServer(connectionString));
                    break;

                case "postgresql":
                    services.AddDbContext<Context>(options => options.UseNpgsql(connectionString)); 
                    break;

                case "mysql":
                    services.AddDbContext<Context>(options => options.UseMySql(connectionString));
                    break;
                default:
                    services.AddDbContext<Context>();
                    break;
                    
            } 

            services.AddControllers()
                        .AddNewtonsoftJson(options => 
                        options.SerializerSettings.ReferenceLoopHandling = 
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddSwaggerDocument();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                // app.UseHsts();
            }

            app.UseHttpsRedirection(); 
            app.UseRouting(); 
            app.UseAuthorization(); 
            app.UseOpenApi();
            app.UseSwaggerUi3(); 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
