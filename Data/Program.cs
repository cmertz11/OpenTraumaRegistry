using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;

namespace TraumaRegistry.Data
{
    public class Program
    {
 

        public IConfiguration Configuration { get; }
       
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("appsettings.json");

            IConfiguration Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TraumaRegistryData;Trusted_Connection=True;ConnectRetryCount=0");

            using (Context ctx = new Context(optionsBuilder.Options))
            {
                 DbInitializer.Initialize(ctx);
            }
        }
    }
}
