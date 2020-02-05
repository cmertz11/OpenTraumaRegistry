using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.AspNetCore.Identity;
using OpenTraumaRegistry.Shared;

namespace OpenTraumaRegistry.Data
{
    public class Program
    {


        public IConfiguration Configuration { get; }
        public static string UserEmail;
        public static string Password { get; set; }

        public static string FirstName { get; set; }
        public static string LastName { get; set; }
        public static string PasswordConfirm { get; set; } = "*";  // Ensure Password and PasswordConfirm do not match prior initial interation of password capture loop.
        public static string FacilityName { get; set; }
        public static bool ArgsCorrect { get; set; } = false;
        public static bool PasswordFormatValid { get; set; } = false;
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("appsettings.json")
                          .AddUserSecrets<Program>().Build();

           IConfiguration Configuration = builder;

            if (args.Length == 3)
            {
                UserEmail = args[0];
                Password = args[1];
                FacilityName = args[2];
                //TODO: all parms aren't handled here
            }
            else
            {
                RequestSetupInfo(Configuration);
            }

          
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
                 DbInitializer.Initialize(ctx, UserEmail, Password, FirstName, LastName, FacilityName, Configuration);
            }
        }

        private static void RequestSetupInfo(IConfiguration configuration)
        {
            
            SecurityHelper SecurityHelper = new SecurityHelper(configuration);
            while (!ArgsCorrect)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Open Trauma Registry Database Setup.");
                Console.WriteLine("");
                Console.WriteLine("Setup needs some information before it can create your database.");

                while (!SecurityHelper.ValidEmailFormat(UserEmail))
                {
                    Console.WriteLine("");
                    Console.WriteLine("System Administrator Email:");
                    UserEmail = Console.ReadLine();
                }


                while (Password != PasswordConfirm)
                {
                    while (!PasswordFormatValid)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Enter System Administrator Password:");
                        Password = SecurityHelper.GetPasswordHidden();
                        PasswordFormatValid = SecurityHelper.ValididatePasswordFormat(Password);
                        if (!PasswordFormatValid)
                        {
                            Console.WriteLine();
                            Console.WriteLine("The password must be at least 6 characters long, contain at least one digit and contain at least one lower case or upper case alpha character");
                        }
                    }

                    Console.WriteLine();
                    Console.WriteLine("Re enter Password:");
                    PasswordConfirm = SecurityHelper.GetPasswordHidden();
                    if (PasswordConfirm != Password)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Passwords do not match.");
                        Console.WriteLine("");
                        PasswordFormatValid = false;
                    }
                }

                Console.WriteLine("");
                while (string.IsNullOrEmpty(FirstName))
                {
                    Console.WriteLine("Enter your First Name:");
                    FirstName = Console.ReadLine();
                }

                Console.WriteLine("");
                while (string.IsNullOrEmpty(LastName))
                {
                    Console.WriteLine("Enter your Last Name:");
                    LastName = Console.ReadLine();
                }

                Console.WriteLine(""); 
                while (string.IsNullOrEmpty(FacilityName))
                {
                    Console.WriteLine("Enter the Name of the Facility:");
                    FacilityName = Console.ReadLine();
                } 
                Console.WriteLine("");
                Console.WriteLine("Your Choices: ");
                Console.WriteLine("");
                Console.WriteLine(string.Format("System Administrator Email: {0}", UserEmail));
                Console.WriteLine("");

                Console.WriteLine(string.Format("First Name: {0}", FirstName));
                Console.WriteLine("");

                Console.WriteLine(string.Format("Last Name: {0}", LastName));
                Console.WriteLine("");

                Console.WriteLine(string.Format("Name of the Facility: {0}", FacilityName));
                Console.WriteLine("");
                Console.WriteLine("Is the correct Y or N?");
                var strRespnse = Console.ReadLine();
                if (strRespnse.ToUpper() == "Y")
                {
                    ArgsCorrect = true;
                }
                else
                {
                    UserEmail = "";
                    Password = "";
                    PasswordConfirm = "*";
                    FacilityName = "";
                    PasswordFormatValid = false;
                }
            }
        }
    }
}
