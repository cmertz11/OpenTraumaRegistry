using System;
using System.Collections.Generic;
using System.Linq;
using TraumaRegistry.Data.Models;


namespace TraumaRegistry.Data
{
    public static class DbInitializer
    {
        public static void Initialize(Context context)
        {
            try
            {
                context.Database.EnsureCreated();
                print("Database Created Successfully.  Starting data load process...");
                print("Loading RefGender");
                if (!context.RefGender.Any())
                { 
                    context.RefGender.Add(new Models.RefGender { Code = "M", Description = "Male" });
                    context.RefGender.Add(new Models.RefGender { Code = "F", Description = "Female" });
                    context.SaveChanges();
                }
                print("Loading RefRace");
                if (!context.RefRace.Any())
                {
                    context.RefRace.Add(new RefRace { Code = "A", Description = "Asian" });
                    context.RefRace.Add(new RefRace { Code = "B", Description = "Black" });
                    context.RefRace.Add(new RefRace { Code = "H", Description = "Hispanic" });
                    context.RefRace.Add(new RefRace { Code = "I", Description = "Native American" });
                    context.RefRace.Add(new RefRace { Code = "O", Description = "Other" });
                    context.RefRace.Add(new RefRace { Code = "W", Description = "White" });
                    context.RefRace.Add(new RefRace { Code = "P", Description = "Polinesian" });
                    context.SaveChanges();
                }
                print("Loading RefInjuryType");
                if (!context.RefInjuryType.Any())
                {
                    context.RefInjuryType.Add(new RefInjuryType { Code = "", Description = "Blunt" });
                    context.RefInjuryType.Add(new RefInjuryType { Code = "", Description = "Penetrating" });
                    context.RefInjuryType.Add(new RefInjuryType { Code = "", Description = "Burn" });
                    context.SaveChanges();
                }
                print("Loading RefRiskData");
                if (!context.RefRiskData.Any())
                {
                    context.RefRiskData.Add(new RefRiskData { Code = "", Description = "Advanced Directive LC" });
                    context.RefRiskData.Add(new RefRiskData { Code = "", Description = "ADD_ADHD" });
                    context.RefRiskData.Add(new RefRiskData { Code = "", Description = "ANGINA" });
                    context.RefRiskData.Add(new RefRiskData { Code = "", Description = "ARREST" });
                    context.RefRiskData.Add(new RefRiskData { Code = "", Description = "BLEEDING DISORDER" });
                    context.RefRiskData.Add(new RefRiskData { Code = "", Description = "DISSEM CANCER" });
                    context.RefRiskData.Add(new RefRiskData { Code = "", Description = "CHEMO" });
                    context.RefRiskData.Add(new RefRiskData { Code = "", Description = "CIRRHOSIS" });
                    context.RefRiskData.Add(new RefRiskData { Code = "", Description = "COPD" });
                    context.RefRiskData.Add(new RefRiskData { Code = "", Description = "CHRONIC RENAL FAILURE" });
                    context.RefRiskData.Add(new RefRiskData { Code = "", Description = "CVA W NEURO DEFICIT" });
                    context.RefRiskData.Add(new RefRiskData { Code = "NTDB", Description = "DEMENTIA" });
                    context.RefRiskData.Add(new RefRiskData { Code = "NTDB", Description = "DEP HEALTH STATUS" });
                    context.RefRiskData.Add(new RefRiskData { Code = "", Description = "DIALYSIS" });
                    context.RefRiskData.Add(new RefRiskData { Code = "NTDB", Description = "DIABETES" });
                    context.RefRiskData.Add(new RefRiskData { Code = "", Description = "DRUG" });
                    context.RefRiskData.Add(new RefRiskData { Code = "NTDB", Description = "ETOH" });
                    context.RefRiskData.Add(new RefRiskData { Code = "NTDB", Description = "HTN" });
                    context.RefRiskData.Add(new RefRiskData { Code = "", Description = "MI" });
                    context.RefRiskData.Add(new RefRiskData { Code = "", Description = "PSYCH" });
                    context.RefRiskData.Add(new RefRiskData { Code = "", Description = "PAD" });
                    context.RefRiskData.Add(new RefRiskData { Code = "NTDB", Description = "SMOKER" });
                    context.RefRiskData.Add(new RefRiskData { Code = "", Description = "STEROID" });
                    context.RefRiskData.Add(new RefRiskData { Code = "", Description = "WARFARIN" });
                    context.RefRiskData.Add(new RefRiskData { Code = "NTDB", Description = "ASA" });
                    context.RefRiskData.Add(new RefRiskData { Code = "", Description = "PLAVIX" });
                    context.RefRiskData.Add(new RefRiskData { Code = "", Description = "BETA" });
                    context.RefRiskData.Add(new RefRiskData { Code = "", Description = "STATIN" });
                    context.RefRiskData.Add(new RefRiskData { Code = "", Description = "DIRECT THROMBIN INHIBITOR" });
                    context.RefRiskData.Add(new RefRiskData { Code = "", Description = "FACTOR XA INHIBITOR" });
                    context.RefRiskData.Add(new RefRiskData { Code = "", Description = "ANTICOAGULATION THERAPY" });

                    context.SaveChanges();
                }
                print("Loading RefSafetyDevices");
                if (!context.RefSafetyDevices.Any())
                {
                    context.RefSafetyDevices.Add(new RefSafetyDevices { Description = "Airbag" });
                    context.RefSafetyDevices.Add(new RefSafetyDevices { Description = "Lap belt" });
                    context.RefSafetyDevices.Add(new RefSafetyDevices { Description = "Shoulder belt" });
                    context.RefSafetyDevices.Add(new RefSafetyDevices { Description = "Child restraint" });
                    context.RefSafetyDevices.Add(new RefSafetyDevices { Description = "Eye protection" });
                    context.RefSafetyDevices.Add(new RefSafetyDevices { Description = "Pads" });
                    context.RefSafetyDevices.Add(new RefSafetyDevices { Description = "Helmet" });
                    context.RefSafetyDevices.Add(new RefSafetyDevices { Description = "Protective clothing" });
                    context.RefSafetyDevices.Add(new RefSafetyDevices { Description = "None/unknown" });

                    context.SaveChanges();
                }
                print("Loading RefLocation");
                if (!context.RefLocation.Any())
                {
                    context.RefLocation.Add(new RefLocation { Code = "ED", Description = "Emergency Department" });
                    context.RefLocation.Add(new RefLocation { Code = "ICU", Description = "Intensive Care Unit" });
                    context.RefLocation.Add(new RefLocation { Code = "TB1", Description = "Trauma Bay 1" });
                    context.RefLocation.Add(new RefLocation { Code = "TB2", Description = "Trauma Bay 2" });

                    context.SaveChanges();
                }
                print("Loading RefTransport");
                if (!context.RefTransport.Any())
                {
                    context.RefTransport.Add(new RefTransport { Code = "POV", Description = "Privately Owned vehicle" });
                    context.RefTransport.Add(new RefTransport { Code = "EMS", Description = "Emergency Medical Service" });

                    context.SaveChanges();
                }
                print("Loading RefArrivedFrom");
                if (!context.RefArrivedFrom.Any())
                {
                    context.RefArrivedFrom.Add(new RefArrivedFrom { Code = "SCENE", Description = "SCENE" });
                    context.RefArrivedFrom.Add(new RefArrivedFrom { Code = "HOME", Description = "HOME" });

                    context.SaveChanges();
                }
                print("Loading RefTraumaLevel");
                if (!context.RefTraumaLevel.Any())
                {
                    context.RefTraumaLevel.Add(new RefTraumaLevel { Code = "F", Description = "FULL" });
                    context.RefTraumaLevel.Add(new RefTraumaLevel { Code = "P", Description = "PARTIAL" });
                    context.RefTraumaLevel.Add(new RefTraumaLevel { Code = "C", Description = "CONSULT" });

                    context.SaveChanges();
                }
                print("Loading RefAgency");
                if (!context.RefAgency.Any())
                {
                    context.RefAgency.Add(new RefAgency { AgencyName = "Mackinaw Emergency Transport", Address1 = "115 Ducharme St.", Address2 = "", City = "Mackinaw City", Phone = "555-889-4765", Zip = "49701", Zip4 = "4586" });

                    context.SaveChanges();
                }

                print("Loading RefPhysicians");
                if (!context.RefPhysicians.Any())
                {
                    context.RefPhysicians.Add(new RefPhysician { LastName = "Strange", FirstName = "Stephen", MI = "V"});
                    context.RefPhysicians.Add(new RefPhysician { LastName = "Robert", FirstName = "Banner", MI = "B" });

                    context.SaveChanges();
                }


                print("Begin load of larger or advanced data sets.  This may take a while.");
                LoadTestData(context);
                print("Data has been successfully loaded.");
            }
            catch (Exception ex)
            {
                print(ex.ToString());
                throw;
            }
        }

        private static void print(string message)
        {
            Console.WriteLine(System.DateTime.Now.ToString() + " - " + message);
        }

        private static void LoadICD10Codes(Context context)
        {
            string filePath = @"C:\Users\cmert\Source\Repos\TraumaRegistry\Data\DBLoadFiles\icd10cm_codes_2020.txt";
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(filePath);
            while ((line = file.ReadLine()) != null)
            {
                var rec = line.Split('|');
                context.RefICD10Codes.Add(new RefICD10 { Code = rec[0].Trim(), Description = rec[1].Trim() });
            }
        }

        private static void LoadTestData(Context context)
        {
            print("Loading ICD10 Codes");
            LoadICD10Codes(context);

            print("Loading Units");
            loadTestUnits(context);

            print("Loading Test Patients");
            loadTestPatients(context);
        }

        private static void loadTestUnits(Context context)
        {
            //throw new NotImplementedException();
            print("NOT IMPLEMENTED");
        }

        private static void loadTestPatients(Context context)
        {

            AddPatient1(context);
            AddPatient2(context);
            AddPatient3(context);
            AddPatient4(context);
            AddPatient5(context);
            AddPatient6(context);
            AddPatient7(context);
            AddPatient8(context);
            AddPatient9(context);
            AddPatient10(context);

            context.SaveChanges();
            for (int i = 0; i < 200; i++)
            {
                AddPatientRND(context , i);
                context.SaveChanges();
            }

        }

        private static void AddPatient1(Context context)
        {
            print("Adding Test Patient 1");
            var patient = new Patient { FirstName = "Natasha", LastName = "Romanoff", MI = "L", DOB = new DateTime(1984, 6, 28), GenderReferenceId = 2, RaceReferenceId = 6 };

            print("Adding Test Event 1 for Patient 1");
            Event event1 = AddPatientEvent1();

            patient.Events.Add(event1);
            context.Patients.Add(patient);
            context.SaveChanges();
        }

        private static void AddPatient2(Context context)
        {
            print("Adding Test Patient 2");
            var patient = new Patient { FirstName = "Steve", LastName = "Rogers", MI = "J", DOB = new DateTime(1918, 7, 4), GenderReferenceId = 1, RaceReferenceId = 6 };

            print("Adding Test Event 1 for Patient 2");
            Event event1 = AddPatientEvent1();

            patient.Events.Add(event1);
            context.Patients.Add(patient);
            context.SaveChanges();
        }

        private static void AddPatient3(Context context)
        {
            print("Adding Test Patient 3");
            var patient = new Patient { FirstName = "Carol", LastName = "Danvers", MI = "H", DOB = new DateTime(1961, 2, 12), GenderReferenceId = 2, RaceReferenceId = 6 };

            print("Adding Test Event 1 for Patient 3");
            Event event1 = AddPatientEvent1();

            patient.Events.Add(event1);
            context.Patients.Add(patient);
            context.SaveChanges();
        }

        private static void AddPatient4(Context context)
        {
            print("Adding Test Patient 4");
            var patient = new Patient { FirstName = "Tony", LastName = "Stark", MI = "", DOB = new DateTime(1970, 5, 29), GenderReferenceId = 1, RaceReferenceId = 6 };

            print("Adding Test Event 1 for Patient 4");
            Event event1 = AddPatientEvent1();

            patient.Events.Add(event1);
            context.Patients.Add(patient);
            context.SaveChanges();
        }

        private static void AddPatient5(Context context)
        {
            print("Adding Test Patient 5");
            var patient = new Patient { FirstName = "Wanda", LastName = "Maximoff", MI = "I", DOB = new DateTime(1999, 10, 14), GenderReferenceId = 2, RaceReferenceId = 6 };

            print("Adding Test Event 1 for Patient 5");
            Event event1 = AddPatientEvent1();

            patient.Events.Add(event1);
            context.Patients.Add(patient);
            context.SaveChanges();
        }

        private static void AddPatient6(Context context)
        {
            print("Adding Test Patient 6");
            var patient = new Patient { FirstName = "Hope", LastName = "Van Dyne", MI = "", DOB = new DateTime(1982, 10, 2), GenderReferenceId = 1, RaceReferenceId = 6 }; 

            print("Adding Test Event 1 for Patient 6");
            Event event1 = AddPatientEvent1();

            patient.Events.Add(event1);
            context.Patients.Add(patient);
            context.SaveChanges();
        }

        private static void AddPatient7(Context context)
        {
            print("Adding Test Patient 7");
                var patient = new Patient { FirstName = "Nick", LastName = "Fury", MI = "V", DOB = new DateTime(1951, 12, 21), GenderReferenceId = 1, RaceReferenceId = 1 };

            print("Adding Test Event 1 for Patient 7");
            Event event1 = AddPatientEvent1();

            patient.Events.Add(event1);
            context.Patients.Add(patient);
            context.SaveChanges();
        }

        private static void AddPatient8(Context context)
        {
            print("Adding Test Patient 8");
            var patient = new Patient { FirstName = "Samuel", LastName = "Wilson", MI = "T", DOB = new DateTime(1971, 12, 21), GenderReferenceId = 1, RaceReferenceId = 1 };

            print("Adding Test Event 1 for Patient 8");
            Event event1 = AddPatientEvent1();

            patient.Events.Add(event1);
            context.Patients.Add(patient);
            context.SaveChanges();
        }

        private static void AddPatient9(Context context)
        {
            print("Adding Test Patient 9");
            var patient = new Patient { FirstName = "Clint", LastName = "Barton", MI = "", DOB = new DateTime(1971, 12, 21), GenderReferenceId = 1, RaceReferenceId = 6 };

            print("Adding Test Event 1 for Patient 9");
            Event event1 = AddPatientEvent1();

            patient.Events.Add(event1);
            context.Patients.Add(patient);
            context.SaveChanges();
        }

        private static void AddPatient10(Context context)
        {
            print("Adding Test Patient 10");
            var patient = new Patient { FirstName = "James", LastName = "Rhodes", MI = "", DOB = new DateTime(1968, 12, 21), GenderReferenceId = 1, RaceReferenceId = 1 };

            print("Adding Test Event 1 for Patient 10");
            Event event1 = AddPatientEvent1();

            patient.Events.Add(event1);
            context.Patients.Add(patient);
            context.SaveChanges();
        }

        private static void AddPatientRND(Context context, int count)
        {
            NameGenerator ngen = new NameGenerator();
            Random rnd = new Random();
            int y = rnd.Next(1940, System.DateTime.Now.Year);
            int m = rnd.Next(1, 12);
            int d = rnd.Next(1, 30);
            int f = rnd.Next(1, 2);
            DateTime dob = GetRandomDOB();

            string FirstName = f == 2 ? ngen.GenRandomFirstNameFemale() : ngen.GenRandomFirstNameMale();
            string LastName = ngen.GenRandomLastName();
            string MI = ngen.GenRandomMI();
            print("Adding Test Patient " + count );
            var patient = new Patient { FirstName = FirstName, LastName = LastName, MI = MI, DOB = dob, GenderReferenceId = f, RaceReferenceId = 1 };

            print("Adding Test Event 1 for Patient 10");
            Event event1 = AddPatientEvent1();

            patient.Events.Add(event1);
            context.Patients.Add(patient);
            context.SaveChanges();
        }

        private static DateTime GetRandomDOB()
        {
            try
            {
                Dictionary<int, int> monthDays = new Dictionary<int, int>();
                monthDays.Add(1, 31);
                monthDays.Add(2, 28);
                monthDays.Add(3, 31);
                monthDays.Add(4, 30);
                monthDays.Add(5, 31);
                monthDays.Add(6, 30);
                monthDays.Add(7, 31);
                monthDays.Add(8, 31);
                monthDays.Add(9, 30);
                monthDays.Add(10, 31);
                monthDays.Add(11, 30);
                monthDays.Add(12, 31);
                Random rnd = new Random();
                int y = rnd.Next(1940, System.DateTime.Now.Year);
                int m = rnd.Next(1, 12);
                int maxDays;
                monthDays.TryGetValue(m, out maxDays);
                int d = rnd.Next(1, maxDays); 
                return new DateTime(y, m, d);
            }
            catch  
            {
                return new DateTime(1970, 10, 3);
            }
        }

        private static Event AddPatientEvent1()
        {
            var event1 = new Event { InjuryDateTime = new DateTime(2004, 7, 2, 21, 35, 1) };

            var injury1p1 = new Injury { AISCode = "541820.2", ICD10 = 102, Diagnosis = "Accidental puncture and laceration of the spleen during a procedure on the spleen" };
            var injury2p1 = new Injury { AISCode = "541822.2", ICD10 = 106, Diagnosis = "Laceration of liver, unspecified degree, initial encounter" };
 
            event1.Injuries.Add(injury1p1);
            event1.Injuries.Add(injury2p1);

            var vitals1 = new Vitals { Diastolic = 76, Systolic = 119, Pulse = 102, RespiratoryRate = 30, SPO2 = 91, Temperature = "97.5", Location = 1, TimeTaken = new DateTime(2004, 7, 2, 22, 06, 8) };
            var vitals2 = new Vitals { Diastolic = 81, Systolic = 121, Pulse = 99, RespiratoryRate = 25, SPO2 = 93, Temperature = "98.1", Location = 1, TimeTaken = new DateTime(2004, 7, 2, 23, 06, 8) };
            var vitals3 = new Vitals { Diastolic = 76, Systolic = 119, Pulse = 102, RespiratoryRate = 30, SPO2 = 91, Temperature = "97.5", Location = 1, TimeTaken = new DateTime(2004, 7, 3, 02, 36, 10) };
            var vitals4 = new Vitals { Diastolic = 76, Systolic = 119, Pulse = 102, RespiratoryRate = 30, SPO2 = 91, Temperature = "97.5", Location = 1, TimeTaken = new DateTime(2004, 7, 3, 03, 05, 23) };

            event1.Vitals.Add(vitals1);
            event1.Vitals.Add(vitals2);
            event1.Vitals.Add(vitals3);
            event1.Vitals.Add(vitals4);
            var consult1 = new Consult { ConsultPhysicianId = 1 };
            event1.Consults.Add(consult1);

          

            var risk1 = new RiskData { RefRiskDataId = 9 };
            var risk2 = new RiskData { RefRiskDataId = 30 };

            event1.Risks.Add(risk1);
            event1.Risks.Add(risk2);

            var safetyDevice1 = new SafetyDevices { RefSafetyDeviceId = 1 };
            var safetyDevice2 = new SafetyDevices { RefSafetyDeviceId = 2 };

            event1.SafetyDevices.Add(safetyDevice1);
            event1.SafetyDevices.Add(safetyDevice2);

            event1.ArrivedFromId = 2;
            event1.TransportId = 1;
            event1.AgencyPreHospitalId = 1;
            event1.TraumaLevelId = 1;

            var injuryType1 = new InjuryTypes { RefInjuryTypeId = 2 };
            event1.InjuryTypes.Add(injuryType1);
            return event1;
        }
    }
    public class NameGenerator
    {
        public Random rnd = new Random();
        public string GenRandomLastName()
        {
            List<string> lst = new List<string>();
            string str = string.Empty;
            lst.Add("Smith");
            lst.Add("Johnson");
            lst.Add("Williams");
            lst.Add("Jones");
            lst.Add("Brown");
            lst.Add("Davis");
            lst.Add("Miller");
            lst.Add("Wilson");
            lst.Add("Moore");
            lst.Add("Taylor");
            lst.Add("Anderson");
            lst.Add("Thomas");
            lst.Add("Jackson");
            lst.Add("White");
            lst.Add("Harris");
            lst.Add("Martin");
            lst.Add("Thompson");
            lst.Add("Garcia");
            lst.Add("Martinez");
            lst.Add("Robinson");
            lst.Add("Clark");
            lst.Add("Rodriguez");
            lst.Add("Lewis");
            lst.Add("Lee");
            lst.Add("Walker");
            lst.Add("Hall");
            lst.Add("Allen");
            lst.Add("Young");
            lst.Add("Hernandez");
            lst.Add("King");
            lst.Add("Wright");
            lst.Add("Lopez");
            lst.Add("Hill");
            lst.Add("Scott");
            lst.Add("Green");
            lst.Add("Adams");
            lst.Add("Baker");
            lst.Add("Gonzalez");
            lst.Add("Nelson");
            lst.Add("Carter");
            lst.Add("Mitchell");
            lst.Add("Perez");
            lst.Add("Roberts");
            lst.Add("Turner");
            lst.Add("Phillips");
            lst.Add("Campbell");
            lst.Add("Parker");
            lst.Add("Evans");
            lst.Add("Edwards");
            lst.Add("Collins");
            lst.Add("Stewart");
            lst.Add("Sanchez");
            lst.Add("Morris");
            lst.Add("Rogers");
            lst.Add("Reed");
            lst.Add("Cook");
            lst.Add("Morgan");
            lst.Add("Bell");
            lst.Add("Murphy");
            lst.Add("Bailey");
            lst.Add("Rivera");
            lst.Add("Cooper");
            lst.Add("Richardson");
            lst.Add("Cox");
            lst.Add("Howard");
            lst.Add("Ward");
            lst.Add("Torres");
            lst.Add("Peterson");
            lst.Add("Gray");
            lst.Add("Ramirez");
            lst.Add("James");
            lst.Add("Watson");
            lst.Add("Brooks");
            lst.Add("Kelly");
            lst.Add("Sanders");
            lst.Add("Price");
            lst.Add("Bennett");
            lst.Add("Wood");
            lst.Add("Barnes");
            lst.Add("Ross");
            lst.Add("Henderson");
            lst.Add("Coleman");
            lst.Add("Jenkins");
            lst.Add("Perry");
            lst.Add("Powell");
            lst.Add("Long");
            lst.Add("Patterson");
            lst.Add("Hughes");
            lst.Add("Flores");
            lst.Add("Washington");
            lst.Add("Butler");
            lst.Add("Simmons");
            lst.Add("Foster");
            lst.Add("Gonzales");
            lst.Add("Bryant");
            lst.Add("Alexander");
            lst.Add("Russell");
            lst.Add("Griffin");
            lst.Add("Diaz");
            lst.Add("Hayes");

            str = lst.OrderBy(xx => rnd.Next()).First();
            return str;
        }
        public string GenRandomFirstNameMale()
        {
            List<string> lst = new List<string>();
            string str = string.Empty;
            lst.Add("Aiden");
            lst.Add("Jackson");
            lst.Add("Mason");
            lst.Add("Liam");
            lst.Add("Jacob");
            lst.Add("Jayden");
            lst.Add("Ethan");
            lst.Add("Noah");
            lst.Add("Lucas");
            lst.Add("Logan");
            lst.Add("Caleb");
            lst.Add("Caden");
            lst.Add("Jack");
            lst.Add("Ryan");
            lst.Add("Connor");
            lst.Add("Michael");
            lst.Add("Elijah");
            lst.Add("Brayden");
            lst.Add("Benjamin");
            lst.Add("Nicholas");
            lst.Add("Alexander");
            lst.Add("William");
            lst.Add("Matthew");
            lst.Add("James");
            lst.Add("Landon");
            lst.Add("Nathan");
            lst.Add("Dylan");
            lst.Add("Evan");
            lst.Add("Luke");
            lst.Add("Andrew");
            lst.Add("Gabriel");
            lst.Add("Gavin");
            lst.Add("Joshua");
            lst.Add("Owen");
            lst.Add("Daniel");
            lst.Add("Carter");
            lst.Add("Tyler");
            lst.Add("Cameron");
            lst.Add("Christian");
            lst.Add("Wyatt");
            lst.Add("Henry");
            lst.Add("Eli");
            lst.Add("Joseph");
            lst.Add("Max");
            lst.Add("Isaac");
            lst.Add("Samuel");
            lst.Add("Anthony");
            lst.Add("Grayson");
            lst.Add("Zachary");
            lst.Add("David");
            lst.Add("Christopher");
            lst.Add("John");
            lst.Add("Isaiah");
            lst.Add("Levi");
            lst.Add("Jonathan");
            lst.Add("Oliver");
            lst.Add("Chase");
            lst.Add("Cooper");
            lst.Add("Tristan");
            lst.Add("Colton");
            lst.Add("Austin");
            lst.Add("Colin");
            lst.Add("Charlie");
            lst.Add("Dominic");
            lst.Add("Parker");
            lst.Add("Hunter");
            lst.Add("Thomas");
            lst.Add("Alex");
            lst.Add("Ian");
            lst.Add("Jordan");
            lst.Add("Cole");
            lst.Add("Julian");
            lst.Add("Aaron");
            lst.Add("Carson");
            lst.Add("Miles");
            lst.Add("Blake");
            lst.Add("Brody");
            lst.Add("Adam");
            lst.Add("Sebastian");
            lst.Add("Adrian");
            lst.Add("Nolan");
            lst.Add("Sean");
            lst.Add("Riley");
            lst.Add("Bentley");
            lst.Add("Xavier");
            lst.Add("Hayden");
            lst.Add("Jeremiah");
            lst.Add("Jason");
            lst.Add("Jake");
            lst.Add("Asher");
            lst.Add("Micah");
            lst.Add("Jace");
            lst.Add("Brandon");
            lst.Add("Josiah");
            lst.Add("Hudson");
            lst.Add("Nathaniel");
            lst.Add("Bryson");
            lst.Add("Ryder");
            lst.Add("Justin");
            lst.Add("Bryce");
            str = lst.OrderBy(xx => rnd.Next()).First();
            return str;
        }
        //—————female
        public string GenRandomFirstNameFemale()
        {
            List<string> lst = new List<string>();
            string str = string.Empty;
            lst.Add("Sophia");
            lst.Add("Emma");
            lst.Add("Isabella");
            lst.Add("Olivia");
            lst.Add("Ava");
            lst.Add("Lily");
            lst.Add("Chloe");
            lst.Add("Madison");
            lst.Add("Emily");
            lst.Add("Abigail");
            lst.Add("Addison");
            lst.Add("Mia");
            lst.Add("Madelyn");
            lst.Add("Ella");
            lst.Add("Hailey");
            lst.Add("Kaylee");
            lst.Add("Avery");
            lst.Add("Kaitlyn");
            lst.Add("Riley");
            lst.Add("Aubrey");
            lst.Add("Brooklyn");
            lst.Add("Peyton");
            lst.Add("Layla");
            lst.Add("Hannah");
            lst.Add("Charlotte");
            lst.Add("Bella");
            lst.Add("Natalie");
            lst.Add("Sarah");
            lst.Add("Grace");
            lst.Add("Amelia");
            lst.Add("Kylie");
            lst.Add("Arianna");
            lst.Add("Anna");
            lst.Add("Elizabeth");
            lst.Add("Sophie");
            lst.Add("Claire");
            lst.Add("Lila");
            lst.Add("Aaliyah");
            lst.Add("Gabriella");
            lst.Add("Elise");
            lst.Add("Lillian");
            lst.Add("Samantha");
            lst.Add("Makayla");
            lst.Add("Audrey");
            lst.Add("Alyssa");
            lst.Add("Ellie");
            lst.Add("Alexis");
            lst.Add("Isabelle");
            lst.Add("Savannah");
            lst.Add("Evelyn");
            lst.Add("Leah");
            lst.Add("Keira");
            lst.Add("Allison");
            lst.Add("Maya");
            lst.Add("Lucy");
            lst.Add("Sydney");
            lst.Add("Taylor");
            lst.Add("Molly");
            lst.Add("Lauren");
            lst.Add("Harper");
            lst.Add("Scarlett");
            lst.Add("Brianna");
            lst.Add("Victoria");
            lst.Add("Liliana");
            lst.Add("Aria");
            lst.Add("Kayla");
            lst.Add("Annabelle");
            lst.Add("Gianna");
            lst.Add("Kennedy");
            lst.Add("Stella");
            lst.Add("Reagan");
            lst.Add("Julia");
            lst.Add("Bailey");
            lst.Add("Alexandra");
            lst.Add("Jordyn");
            lst.Add("Nora");
            lst.Add("Carolin");
            lst.Add("Mackenzie");
            lst.Add("Jasmine");
            lst.Add("Jocelyn");
            lst.Add("Kendall");
            lst.Add("Morgan");
            lst.Add("Nevaeh");
            lst.Add("Maria");
            lst.Add("Eva");
            lst.Add("Juliana");
            lst.Add("Abby");
            lst.Add("Alexa");
            lst.Add("Summer");
            lst.Add("Brooke");
            lst.Add("Penelope");
            lst.Add("Violet");
            lst.Add("Kate");
            lst.Add("Hadley");
            lst.Add("Ashlyn");
            lst.Add("Sadie");
            lst.Add("Paige");
            lst.Add("Katherine");
            lst.Add("Sienna");
            lst.Add("Piper");

            str = lst.OrderBy(xx => rnd.Next()).First();
            return str;

        }

        public string GenRandomMI()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 1)
              .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }
    }

}
