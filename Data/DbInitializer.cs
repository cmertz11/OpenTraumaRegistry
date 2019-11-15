using System;
using System.Collections.Generic;
using System.Linq;
using TraumaRegistry.Data.Models;


namespace TraumaRegistry.Data
{
    public static class DbInitializer
    {
        public static string SystemId { get; set; } = Guid.NewGuid().ToString();  //Get System Id from Identity 
        public static void Initialize(Context context, ref string OutputString)
        {
            try
            {
                if(!context.Database.EnsureCreated())
                {
                   print ("The database was not created.  Check if a database with that name already exsist.", ref OutputString);
                   return;
                }
                else
                {
                   print("Database Created Successfully.  Starting data load process...", ref OutputString);
                   print("Starting data load process...", ref OutputString);
                } 

                if (!context.RefGender.Any())
                {
                    print("Loading RefGender", ref OutputString);
                    context.RefGender.Add(new Models.RefGender { Code = "M", Description = "Male" });
                    context.RefGender.Add(new Models.RefGender { Code = "F", Description = "Female" });
                    context.SaveChanges();
                }
                if (!context.RefRace.Any())
                {
                    print("Loading RefRace", ref OutputString);
                    context.RefRace.Add(new RefRace { Code = "A", Description = "Asian" });
                    context.RefRace.Add(new RefRace { Code = "B", Description = "Black" });
                    context.RefRace.Add(new RefRace { Code = "H", Description = "Hispanic" });
                    context.RefRace.Add(new RefRace { Code = "I", Description = "Native American" });
                    context.RefRace.Add(new RefRace { Code = "O", Description = "Other" });
                    context.RefRace.Add(new RefRace { Code = "W", Description = "White" });
                    context.RefRace.Add(new RefRace { Code = "P", Description = "Polinesian" });
                    context.SaveChanges();
                }
                if (!context.RefInjuryType.Any())
                {
                    print("Loading RefInjuryType", ref OutputString); 
                    context.RefInjuryType.Add(new RefInjuryType { Code = "", Description = "Blunt" });
                    context.RefInjuryType.Add(new RefInjuryType { Code = "", Description = "Penetrating" });
                    context.RefInjuryType.Add(new RefInjuryType { Code = "", Description = "Burn" });
                    context.SaveChanges();
                }
                
                if (!context.RefRiskData.Any())
                {
                    print("Loading RefRiskData", ref OutputString);
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
                if (!context.RefSafetyDevices.Any())
                {
                    print("Loading RefSafetyDevices", ref OutputString);
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
                print("Loading RefLocation", ref OutputString);
                if (!context.RefLocation.Any())
                {
                    context.RefLocation.Add(new RefLocation { Code = "ED", Description = "Emergency Department" });
                    context.RefLocation.Add(new RefLocation { Code = "ICU", Description = "Intensive Care Unit" });
                    context.RefLocation.Add(new RefLocation { Code = "TB1", Description = "Trauma Bay 1" });
                    context.RefLocation.Add(new RefLocation { Code = "TB2", Description = "Trauma Bay 2" });

                    context.SaveChanges();
                }
                print("Loading RefTransport", ref OutputString);
                if (!context.RefTransport.Any())
                {
                    context.RefTransport.Add(new RefTransport { Code = "POV", Description = "Privately Owned vehicle" });
                    context.RefTransport.Add(new RefTransport { Code = "EMS", Description = "Emergency Medical Service" });

                    context.SaveChanges();
                }
                print("Loading RefArrivedFrom", ref OutputString);
                if (!context.RefArrivedFrom.Any())
                {
                    context.RefArrivedFrom.Add(new RefArrivedFrom { Code = "SCENE", Description = "SCENE" });
                    context.RefArrivedFrom.Add(new RefArrivedFrom { Code = "HOME", Description = "HOME" });

                    context.SaveChanges();
                }
                print("Loading RefTraumaLevel", ref OutputString);
                if (!context.RefTraumaLevel.Any())
                {
                    context.RefTraumaLevel.Add(new RefTraumaLevel { Code = "F", Description = "FULL" });
                    context.RefTraumaLevel.Add(new RefTraumaLevel { Code = "P", Description = "PARTIAL" });
                    context.RefTraumaLevel.Add(new RefTraumaLevel { Code = "C", Description = "CONSULT" });

                    context.SaveChanges();
                }
                print("Loading RefOutcome", ref OutputString);
                if (!context.RefOutcome.Any())
                {
                    context.RefOutcome.Add(new RefOutcome { Code = "A", Description = "Alive" });
                    context.RefOutcome.Add(new RefOutcome { Code = "D", Description = "Dead" });

                    context.SaveChanges();
                }
                print("Loading RefAgency", ref OutputString);
                if (!context.RefAgency.Any())
                {
                    context.RefAgency.Add(new RefAgency { AgencyName = "Mackinaw Emergency Transport", Address1 = "115 Ducharme St.", Address2 = "", City = "Mackinaw City", Phone = "555-889-4765", Zip = "49701", Zip4 = "4586" });

                    context.SaveChanges();
                }

                print("Loading RefPhysicians", ref OutputString);
                if (!context.RefPhysicians.Any())
                {
                    context.RefPhysicians.Add(new RefPhysician { LastName = "Strange", FirstName = "Stephen", MI = "V" });
                    context.RefPhysicians.Add(new RefPhysician { LastName = "Robert", FirstName = "Banner", MI = "B" });

                    context.SaveChanges();
                }

                print("Loading Reference Table List with Id, Code, Description Schema", ref OutputString);
                if(!context.ReferenceTables.Any())
                {
                    context.ReferenceTables.Add(new ReferenceTables { Code = "RefArrivedFrom", Description = "Arrived From" });
                    context.ReferenceTables.Add(new ReferenceTables { Code = "RefGender", Description = "Gender" });
                    context.ReferenceTables.Add(new ReferenceTables { Code = "RefInjuryType", Description = "Injury Types" });
                    context.ReferenceTables.Add(new ReferenceTables { Code = "RefLocation", Description = "Locations" });
                    context.ReferenceTables.Add(new ReferenceTables { Code = "RefOutcome", Description = "Outcomes" });
                    context.ReferenceTables.Add(new ReferenceTables { Code = "RefRace", Description = "Race" });
                    context.ReferenceTables.Add(new ReferenceTables { Code = "RefRiskData", Description = "Risk Data" });
                    context.ReferenceTables.Add(new ReferenceTables { Code = "RefSafetyDevices", Description = "Safety Devices" });
                    context.ReferenceTables.Add(new ReferenceTables { Code = "RefTransport", Description = "Transport" });
                    context.ReferenceTables.Add(new ReferenceTables { Code = "RefTraumaLevel", Description = "Trauma Level" });
                    context.ReferenceTables.Add(new ReferenceTables { Code = "ReferenceTables", Description = "Reference Tables" });

                    context.SaveChanges();
                }

                print("Begin load of larger or advanced data sets.  This may take a while.", ref OutputString);
                print("Loading ICD10 Codes", ref OutputString);
                LoadICD10Codes(context);

                 LoadTestData(context, ref OutputString);
                print("Database has been successfully Created.", ref OutputString);
            }
            catch (Exception ex)
            {
                print(ex.ToString(), ref OutputString);
                throw;
            }
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

        private static void LoadTestData(Context context, ref string OutputString)
        { 
            print("Loading Units", ref OutputString);
            loadTestUnits(context, ref OutputString);

            print("Loading Test Patients", ref OutputString);
            loadTestPatients(context, ref OutputString);
        }

        private static void loadTestUnits(Context context, ref string OutputString)
        {
            //throw new NotImplementedException();
            print("NOT IMPLEMENTED", ref OutputString);
        }

        private static void loadTestPatients(Context context, ref string OutputString)
        {

            AddPatient1(context, ref  OutputString);
            AddPatient2(context, ref  OutputString);
            AddPatient3(context, ref  OutputString);
            AddPatient4(context, ref  OutputString);
            AddPatient5(context, ref  OutputString);
            AddPatient6(context, ref  OutputString);
            AddPatient7(context, ref  OutputString);
            AddPatient8(context, ref  OutputString);
            AddPatient9(context, ref  OutputString);
            AddPatient10(context, ref  OutputString);

            context.SaveChanges();
            for (int i = 0; i < 20; i++)
            {
                AddPatientRND(context, i, ref OutputString);
                context.SaveChanges();
            }

        }

        private static void AddPatient1(Context context, ref string OutputString)
        {
            print("Adding Test Patient 1", ref OutputString);
            var patient = new Patient { FirstName = "Natasha", LastName = "Romanoff", MI = "L", DOB = new DateTime(1984, 6, 28), GenderReferenceId = 2, RaceReferenceId = 6 , Created = System.DateTime.Now, LastUpdate = System.DateTime.Now, LastUpdatedBy = SystemId};
            patient.email = string.Format("{0}.{1}@gmail.com", patient.FirstName, patient.LastName);
            patient.MRN = GenerateMRN();
            print("Adding Test Event 1 for Patient 1", ref OutputString);
            Event event1 = AddPatientEvent1();
            patient.Events.Add(event1);

            print("Adding Test Event 2 for Patient 1", ref OutputString);
            Event event2 = AddPatientEvent2();
            patient.Events.Add(event2);

            context.Patients.Add(patient);
            context.SaveChanges();
        }


        private static void AddPatient2(Context context, ref string OutputString)
        {
            print("Adding Test Patient 2", ref OutputString);
            var patient = new Patient { FirstName = "Steve", LastName = "Rogers", MI = "J", DOB = new DateTime(1918, 7, 4), GenderReferenceId = 1, RaceReferenceId = 6 };
            patient.email = string.Format("{0}.{1}@gmail.com", patient.FirstName, patient.LastName);
            patient.MRN = GenerateMRN();
            print("Adding Test Event 1 for Patient 2", ref OutputString);
            Event event1 = AddPatientEvent1();

            patient.Events.Add(event1);
            context.Patients.Add(patient);
            context.SaveChanges();
        }

        private static void AddPatient3(Context context, ref string OutputString)
        {
            print("Adding Test Patient 3", ref OutputString);
            var patient = new Patient { FirstName = "Carol", LastName = "Danvers", MI = "H", DOB = new DateTime(1961, 2, 12), GenderReferenceId = 2, RaceReferenceId = 6, Created = System.DateTime.Now, LastUpdate = System.DateTime.Now, LastUpdatedBy = SystemId };
            patient.email = string.Format("{0}.{1}@gmail.com", patient.FirstName, patient.LastName);
            patient.MRN = GenerateMRN();
            print("Adding Test Event 1 for Patient 3", ref OutputString);
            Event event1 = AddPatientEvent1();

            patient.Events.Add(event1);
            context.Patients.Add(patient);
            context.SaveChanges();
        }

        private static void AddPatient4(Context context, ref string OutputString)
        {
            print("Adding Test Patient 4", ref OutputString);
            var patient = new Patient { FirstName = "Tony", LastName = "Stark", MI = "", DOB = new DateTime(1970, 5, 29), GenderReferenceId = 1, RaceReferenceId = 6, Created = System.DateTime.Now, LastUpdate = System.DateTime.Now, LastUpdatedBy = SystemId };
            patient.email = string.Format("{0}.{1}@gmail.com", patient.FirstName, patient.LastName);
            patient.MRN = GenerateMRN();
            print("Adding Test Event 1 for Patient 4", ref OutputString);
            Event event1 = AddPatientEvent1();

            patient.Events.Add(event1);
            context.Patients.Add(patient);
            context.SaveChanges();
        }

        private static void AddPatient5(Context context, ref string OutputString)
        {
            print("Adding Test Patient 5", ref OutputString);
            var patient = new Patient { FirstName = "Wanda", LastName = "Maximoff", MI = "I", DOB = new DateTime(1999, 10, 14), GenderReferenceId = 2, RaceReferenceId = 6, Created = System.DateTime.Now, LastUpdate = System.DateTime.Now, LastUpdatedBy = SystemId };
            patient.email = string.Format("{0}.{1}@gmail.com", patient.FirstName, patient.LastName);
            patient.MRN = GenerateMRN();
            print("Adding Test Event 1 for Patient 5", ref OutputString);
            Event event1 = AddPatientEvent1();

            patient.Events.Add(event1);
            context.Patients.Add(patient);
            context.SaveChanges();
        }

        private static void AddPatient6(Context context, ref string OutputString)
        {
            print("Adding Test Patient 6", ref OutputString);
            var patient = new Patient { FirstName = "Hope", LastName = "Van Dyne", MI = "", DOB = new DateTime(1982, 10, 2), GenderReferenceId = 1, RaceReferenceId = 6, Created = System.DateTime.Now, LastUpdate = System.DateTime.Now, LastUpdatedBy = SystemId };
            patient.email = string.Format("{0}.{1}@gmail.com", patient.FirstName, patient.LastName);
            patient.MRN = GenerateMRN();
            print("Adding Test Event 1 for Patient 6", ref OutputString);
            Event event1 = AddPatientEvent1();

            patient.Events.Add(event1);
            context.Patients.Add(patient);
            context.SaveChanges();
        }

        private static void AddPatient7(Context context, ref string OutputString)
        {
            print("Adding Test Patient 7", ref OutputString);
            var patient = new Patient { FirstName = "Nick", LastName = "Fury", MI = "V", DOB = new DateTime(1951, 12, 21), GenderReferenceId = 1, RaceReferenceId = 1, Created = System.DateTime.Now, LastUpdate = System.DateTime.Now, LastUpdatedBy = SystemId };
            patient.email = string.Format("{0}.{1}@gmail.com", patient.FirstName, patient.LastName);
            patient.MRN = GenerateMRN();
            print("Adding Test Event 1 for Patient 7", ref OutputString);
            Event event1 = AddPatientEvent1();

            patient.Events.Add(event1);
            context.Patients.Add(patient);
            context.SaveChanges();
        }

        private static void AddPatient8(Context context, ref string OutputString)
        {
            print("Adding Test Patient 8", ref OutputString);
            var patient = new Patient { FirstName = "Samuel", LastName = "Wilson", MI = "T", DOB = new DateTime(1971, 12, 21), GenderReferenceId = 1, RaceReferenceId = 1, Created = System.DateTime.Now, LastUpdate = System.DateTime.Now, LastUpdatedBy = SystemId };
            patient.email = string.Format("{0}.{1}@gmail.com", patient.FirstName, patient.LastName);
            patient.MRN = GenerateMRN();
            print("Adding Test Event 1 for Patient 8", ref OutputString);
            Event event1 = AddPatientEvent1();

            patient.Events.Add(event1);
            context.Patients.Add(patient);
            context.SaveChanges();
        }

        private static void AddPatient9(Context context, ref string OutputString)
        {
            print("Adding Test Patient 9", ref OutputString);
            var patient = new Patient { FirstName = "Clint", LastName = "Barton", MI = "", DOB = new DateTime(1971, 12, 21), GenderReferenceId = 1, RaceReferenceId = 6, Created = System.DateTime.Now, LastUpdate = System.DateTime.Now, LastUpdatedBy = SystemId };
            patient.email = string.Format("{0}.{1}@gmail.com", patient.FirstName, patient.LastName);
            patient.MRN = GenerateMRN();
            print("Adding Test Event 1 for Patient 9", ref OutputString);
            Event event1 = AddPatientEvent1();

            patient.Events.Add(event1);
            context.Patients.Add(patient);
            context.SaveChanges();
        }

        private static void AddPatient10(Context context, ref string OutputString)
        {
            print("Adding Test Patient 10", ref OutputString);
            var patient = new Patient { FirstName = "James", LastName = "Rhodes", MI = "", DOB = new DateTime(1968, 12, 21), GenderReferenceId = 1, RaceReferenceId = 1, Created = System.DateTime.Now, LastUpdate = System.DateTime.Now, LastUpdatedBy = SystemId };
            patient.email = string.Format("{0}.{1}@gmail.com", patient.FirstName, patient.LastName);
            patient.MRN = GenerateMRN();
            print("Adding Test Event 1 for Patient 10", ref OutputString);
            Event event1 = AddPatientEvent1();

            patient.Events.Add(event1);
            context.Patients.Add(patient);
            context.SaveChanges();
        }

        private static void AddPatientRND(Context context, int count, ref string OutputString)
        {
            RandomPatientGenerator ngen = new RandomPatientGenerator();
            Random rnd = new Random();
            int y = rnd.Next(1940, System.DateTime.Now.Year);
            int m = rnd.Next(1, 12);
            int d = rnd.Next(1, 30);
            int f = rnd.Next(1, 2);
            DateTime dob = ngen.GetRandomDOB();

            string FirstName = f == 2 ? ngen.GenRandomFirstNameFemale() : ngen.GenRandomFirstNameMale();
            string LastName = ngen.GenRandomLastName();
            string MI = ngen.GenRandomMI();
            print("Adding Test Patient " + count, ref OutputString);
            var patient = new Patient { FirstName = FirstName, LastName = LastName, MI = MI, DOB = dob, GenderReferenceId = f, RaceReferenceId = 1, Created = System.DateTime.Now, LastUpdate = System.DateTime.Now, LastUpdatedBy = SystemId };
            patient.email = string.Format("{0}.{1}@gmail.com", FirstName, LastName);
            patient.MRN = GenerateMRN();
            print("Adding Test Event 1 for Patient " + (count + 10).ToString(), ref OutputString); // the + 10 is to account for the first 10 patients added.
            Event event1 = AddPatientEvent1();

            patient.Events.Add(event1);
            context.Patients.Add(patient);
            context.SaveChanges();
        }

        private static Event AddPatientEvent1()
        {
            var event1 = new Event { InjuryDateTime = new DateTime(2004, 7, 2, 21, 35, 1) };

            event1.PositionInVehicle = "Passenger seat";
            event1.LocationOfOccuranceDescription = "The intersection of M24 and Coulter Rd. north of Lapeer Michigan.";
            event1.InjuryDetailsNarrative = "MVA - Car vs Tree";
            event1.OccuranceZipCode = "48446";

            event1.TimeInERHolder = true;
            event1.FastExam = false;
            event1.FastExamPositive = false;

            event1.TransportId = 1;
            event1.OutcomeId = 1;

            event1.AgencyDispatchDateTime = new DateTime(2004, 7, 2, 22, 05, 4);
            event1.AgencyArriveSceneDateTime = new DateTime(2004, 7, 22, 20, 05, 2);
            event1.AgencyDepartSceneDateTime = new DateTime(2004, 7, 2, 22, 35, 4);
            event1.EDArrivalDateTime = new DateTime(2004, 7, 2, 22, 53, 1);

            event1.ActivationDateTime = new DateTime(2004, 7, 2, 22, 05, 4);  // Talk to Pam about timing of this timestamp.

            event1.EDDischargeDateTime = new DateTime(2004, 7, 3, 3, 05, 4); // Talk to Pam about timing of this timestamp.

            event1.HospitalDischargeOrder = new DateTime(2004, 7, 3, 16, 55, 7); // Talk to Pam about timing of this timestamp.

            var injury1p1 = new Injury { AISCode = "541820.2", ICD10 = 102, Diagnosis = "Accidental puncture and laceration of the spleen during a procedure on the spleen" };
            var injury2p1 = new Injury { AISCode = "541822.2", ICD10 = 106, Diagnosis = "Laceration of liver, unspecified degree, initial encounter" };

            event1.Injuries.Add(injury1p1);
            event1.Injuries.Add(injury2p1);

            var vitals1 = new Vitals { Diastolic = 76, Systolic = 119, Pulse = 102, RespiratoryRate = 30, SPO2 = 91, Temperature = 102.5m, Location = 1, TimeTaken = new DateTime(2004, 7, 2, 22, 06, 8) };
            var vitals2 = new Vitals { Diastolic = 81, Systolic = 121, Pulse = 99, RespiratoryRate = 25, SPO2 = 93, Temperature = 98.1m, Location = 1, TimeTaken = new DateTime(2004, 7, 2, 23, 06, 8) };
            var vitals3 = new Vitals { Diastolic = 76, Systolic = 119, Pulse = 102, RespiratoryRate = 30, SPO2 = 91, Temperature = 97.5m, Location = 1, TimeTaken = new DateTime(2004, 7, 3, 02, 36, 10) };
            var vitals4 = new Vitals { Diastolic = 76, Systolic = 119, Pulse = 102, RespiratoryRate = 30, SPO2 = 91, Temperature = 97.5m, Location = 1, TimeTaken = new DateTime(2004, 7, 3, 03, 05, 23) };

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

        private static Event AddPatientEvent2()
        {
            var event1 = new Event { InjuryDateTime = new DateTime(2010, 7, 2, 21, 35, 1) };

            event1.PositionInVehicle = "";
            event1.LocationOfOccuranceDescription = "Home - 342 S. Main St. Lapeer MI";
            event1.InjuryDetailsNarrative = "Slip and fall on ice in parking lot of apartment building.";
            event1.OccuranceZipCode = "48446";

            event1.TimeInERHolder = true;
            event1.FastExam = false;
            event1.FastExamPositive = false;

            event1.TransportId = 1;
            event1.OutcomeId = 2;

            event1.AgencyDispatchDateTime = new DateTime(2010, 7, 2, 22, 05, 4);
            event1.AgencyArriveSceneDateTime = new DateTime(2010, 7, 22, 20, 05, 2);
            event1.AgencyDepartSceneDateTime = new DateTime(2010, 7, 2, 22, 35, 4);
            event1.EDArrivalDateTime = new DateTime(2010, 7, 2, 22, 53, 1);

            event1.ActivationDateTime = new DateTime(2004, 7, 2, 22, 05, 4);  // Talk to Pam about timing of this timestamp.

            event1.EDDischargeDateTime = new DateTime(2004, 7, 3, 3, 05, 4); // Talk to Pam about timing of this timestamp.

            event1.HospitalDischargeOrder = new DateTime(2004, 7, 3, 16, 55, 7); // Talk to Pam about timing of this timestamp.

            var injury1p1 = new Injury { AISCode = "541820.2", ICD10 = 102, Diagnosis = "Accidental puncture and laceration of the spleen during a procedure on the spleen" };
            var injury2p1 = new Injury { AISCode = "541822.2", ICD10 = 106, Diagnosis = "Laceration of liver, unspecified degree, initial encounter" };

            event1.Injuries.Add(injury1p1);
            event1.Injuries.Add(injury2p1);

            var vitals1 = new Vitals { Diastolic = 76, Systolic = 119, Pulse = 102, RespiratoryRate = 30, SPO2 = 91, Temperature = 97.5m, Location = 1, TimeTaken = new DateTime(2004, 7, 2, 22, 06, 8) };
            var vitals2 = new Vitals { Diastolic = 81, Systolic = 121, Pulse = 99, RespiratoryRate = 25, SPO2 = 93, Temperature =  98.1m, Location = 1, TimeTaken = new DateTime(2004, 7, 2, 23, 06, 8) };
            var vitals3 = new Vitals { Diastolic = 76, Systolic = 119, Pulse = 102, RespiratoryRate = 30, SPO2 = 91, Temperature = 97.5m, Location = 1, TimeTaken = new DateTime(2004, 7, 3, 02, 36, 10) };
            var vitals4 = new Vitals { Diastolic = 76, Systolic = 119, Pulse = 102, RespiratoryRate = 30, SPO2 = 91, Temperature = 97.5m, Location = 1, TimeTaken = new DateTime(2004, 7, 3, 03, 05, 23) };

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

        private static void print(string message, ref string outputString)
        {
            Console.WriteLine(System.DateTime.Now.ToString() + " - " + message);
            outputString += message + Environment.NewLine;
        }
        private static string GenerateMRN()
        {
            Random r = new Random(System.DateTime.Now.Second);
            int rInt = r.Next(0, 100);
            return rInt.ToString().PadLeft(8, '0');
        }

    }
}
