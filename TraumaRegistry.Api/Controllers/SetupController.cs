using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using TraumaRegistry.Data;

namespace TraumaRegistry.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SetupController : ControllerBase
    {
        private IConfiguration _configuration; 
        string outputString = "";
        public SetupController(IConfiguration configuration)
        {
            _configuration = configuration;
  
        }

        [HttpGet]
        [ActionName("SetupDatabase")]
        public string SetupDatabase(string dbProvider, string connectionString)
        {
            if (string.IsNullOrEmpty(dbProvider) || (string.IsNullOrEmpty(connectionString)))
            {
                return "Please provide the Database Provider and Database Connection String.";
            }
            var builder = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json").Build();

            IConfiguration Configuration = builder;

            SaveToAppSettings(dbProvider, connectionString);

            var optionsBuilder = new DbContextOptionsBuilder<Context>();

            switch (dbProvider)
            {
                case "sqlserver":
                    optionsBuilder.UseSqlServer(connectionString); 
                     break;

                case "postgresql":
                    optionsBuilder.UseNpgsql(connectionString); 
                    break;

                case "mysql":
                    optionsBuilder.UseMySql(connectionString);
                    //https://bugs.mysql.com/bug.php?id=96990
                    //throw new Exception("MySQL is not implemented due to mysql bug 96990 for dotnetcore 3.0 ");
                    break;
                default:
                    return "Unable to create database with submitted configuration.";
                     
            }
            try
            { 
                using (Context ctx = new Context(optionsBuilder.Options))
                { 
                    DbInitializer.Initialize(ctx, ref outputString);
                    return outputString;
                }
                
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

             
        }

        private void SaveToAppSettings(string dbProvider, string connectionString)
        {
            string filepath = "appsettings.json";
            string result = string.Empty;
            using (StreamReader r = new StreamReader(filepath))
            {
                var json = r.ReadToEnd();
                var jobj = JObject.Parse(json);

                jobj["TraumaRegistrySettings"]["dbProvider"] = dbProvider;
                jobj["TraumaRegistrySettings"]["connectionString"] = connectionString;
                result = jobj.ToString();
              
            }
            System.IO.File.WriteAllText(filepath, result);
        }

        [HttpGet]
        [ActionName("IsDatabaseConfigured")]
        public string IsDatabaseConfigured()
        {
            var dbProvider = _configuration.GetSection("TraumaRegistrySettings")["dbProvider"];
            var connectionString = _configuration.GetSection("TraumaRegistrySettings")["connectionString"];

            return (!string.IsNullOrEmpty(dbProvider) && !string.IsNullOrEmpty(connectionString)).ToString();

        }

    }


}