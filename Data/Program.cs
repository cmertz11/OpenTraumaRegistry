using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace OpenTraumaRegistry.Data
{
    public class Program
    {
        public IConfiguration Configuration { get; }
       
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("appsettings.json").Build();

            IConfiguration Configuration = builder;

            var dbProvider = Configuration.GetSection("TraumaRegistrySettings")["dbProvider"];
            var connectionString = Configuration.GetSection("TraumaRegistrySettings")["connectionString"];

            var optionsBuilder = new DbContextOptionsBuilder<Context>();

            switch (dbProvider)
            {
                case "sqlserver":
                        optionsBuilder.UseSqlServer(connectionString);
                    break;

                case "postgres":
                        optionsBuilder.UseNpgsql(connectionString);
                    break;

                case "mysql": 
                        optionsBuilder.UseMySql(connectionString);
                        //https://bugs.mysql.com/bug.php?id=96990
                        throw new Exception("MySQL is not implemented due to mysql bug 96990 for dotnetcore 3.0 ");
                default:
                    break;
            }



            using (Context ctx = new Context(optionsBuilder.Options))
            {
                string outputString = "";
                 DbInitializer.Initialize(ctx, ref outputString);
            }
        }
    }
}
