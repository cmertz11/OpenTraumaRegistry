using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using TraumaRegistry.Data;

namespace TraumaRegistry.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SetupController : ControllerBase
    {
        private IConfiguration _configuration;

        public SetupController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [ActionName("SetupDatabase")]
        public string SetupDatabase(string dbProvider, string connectionString)
        {
            var builder = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json").Build();

            IConfiguration Configuration = builder;
            //http://localhost:44392/api/setup/SetupDatabase?dbProvider=sql&connectionString=constr
            //Configuration.GetSection("TraumaRegistrySettings")["dbProvider"] = dbProvider;
            //Configuration.GetSection("TraumaRegistrySettings")["connectionString"] = connectionString;

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
                    optionsBuilder.UseMySQL(connectionString);
                    //https://bugs.mysql.com/bug.php?id=96990
                    throw new Exception("MySQL is not implemented due to mysql bug 96990 for dotnetcore 3.0 ");
                default:
                    return "Unable to create database with submitted configuration.";
                     
            }
            try
            {
                using (Context ctx = new Context(optionsBuilder.Options))
                {
                    string outputString = "";
                    DbInitializer.Initialize(ctx, ref outputString);
                }
                return "Trauma Registry database successfully created!";
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