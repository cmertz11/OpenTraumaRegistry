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

            //var patient2 = (new Patient { FirstName = "Steve", LastName = "Rogers", MI = "J", DOB = new DateTime(1918, 7, 4), GenderReferenceId = 1, RaceReferenceId = 6 });
            //patient2.Events.Add(new Event { InjuryDateTime = new DateTime(2004, 7, 2, 21, 35, 1) });
            //context.Patients.Add(patient2);

            //var patient3 = (new Patient { FirstName = "Carol", LastName = "Danvers", MI = "H", DOB = new DateTime(1961, 2, 12), GenderReferenceId = 2, RaceReferenceId = 6 });
            //patient3.Events.Add(new Event { InjuryDateTime = new DateTime(2004, 7, 2, 21, 35, 1) });
            //context.Patients.Add(patient3);

            //var patient4 = (new Patient { FirstName = "Tony", LastName = "Stark", MI = "", DOB = new DateTime(1970, 5, 29), GenderReferenceId = 1, RaceReferenceId = 6 });
            //patient4.Events.Add(new Event { InjuryDateTime = new DateTime(2004, 7, 2, 21, 35, 1) });
            //context.Patients.Add(patient4);

            //var patient5 = (new Patient { FirstName = "Wanda", LastName = "Maximoff", MI = "I", DOB = new DateTime(1999, 10, 14), GenderReferenceId = 2, RaceReferenceId = 6 });
            //patient5.Events.Add(new Event { InjuryDateTime = new DateTime(2004, 7, 2, 21, 35, 1) });
            //context.Patients.Add(patient5);

            //var patient6 = (new Patient { FirstName = "Nick", LastName = "Fury", MI = "V", DOB = new DateTime(1951, 12, 21), GenderReferenceId = 1, RaceReferenceId = 1 });
            //patient6.Events.Add(new Event { InjuryDateTime = new DateTime(2004, 7, 2, 21, 35, 1) });
            //context.Patients.Add(patient6);

            //var patient7 = (new Patient { FirstName = "Hope", LastName = "Van Dyne", MI = "", DOB = new DateTime(1982, 10, 2), GenderReferenceId = 1, RaceReferenceId = 6 });
            //patient7.Events.Add(new Event { InjuryDateTime = new DateTime(2004, 7, 2, 21, 35, 1) });
            //context.Patients.Add(patient7);

            context.SaveChanges();
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
}
