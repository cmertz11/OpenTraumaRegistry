using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
 

using TraumaRegistry.Data;

namespace TraumaRegistry.Api
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

                case "postgres":
                    services.AddDbContext<Context>(options => options.UseNpgsql(connectionString)); 
                    break;

                case "mysql":
                    services.AddDbContext<Context>(options => options.UseMySQL(connectionString)); 
                    //https://bugs.mysql.com/bug.php?id=96990
                    throw new Exception("MySQL is not implemented due to mysql bug 96990 for dotnetcore 3.0 ");
                default:
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
