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
            context.Database.EnsureCreated();

            if(!context.RefGender.Any()) {  
                
                context.RefGender.Add(new Models.RefGender { Code = "M", Description = "Male" });
                context.RefGender.Add(new Models.RefGender { Code = "F", Description = "Female"});
                context.SaveChanges();
            }

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

            if (!context.RefInjuryType.Any())
            {
                context.RefInjuryType.Add(new RefInjuryType { Code = "", Description = "Blunt" });
                context.RefInjuryType.Add(new RefInjuryType { Code = "", Description = "Penetrating" });
                context.RefInjuryType.Add(new RefInjuryType { Code = "", Description = "Burn" });
                context.SaveChanges();
            }
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

            if (!context.RefLocation.Any())
            {
                context.RefLocation.Add(new RefLocation { Code = "ED", Description = "Emergency Department" });
                context.RefLocation.Add(new RefLocation { Code = "ICU", Description = "Intensive Care Unit" });
                context.RefLocation.Add(new RefLocation { Code = "TB1", Description = "Trauma Bay 1" });
                context.RefLocation.Add(new RefLocation { Code = "TB2", Description = "Trauma Bay 2" });

                context.SaveChanges();
            }

            if (!context.RefTransport.Any())
            {
                context.RefTransport.Add(new RefTransport { Code = "POV", Description = "Privately Owned vehicle"});
                context.RefTransport.Add(new RefTransport { Code = "EMS", Description = "Emergency Medical Service"});

                context.SaveChanges();
            }

            if (!context.RefArrivedFrom.Any())
            {
                context.RefArrivedFrom.Add(new RefArrivedFrom { Code = "SCENE", Description = "SCENE"});
                context.RefArrivedFrom.Add(new RefArrivedFrom { Code = "HOME", Description = "HOME"});

                context.SaveChanges();
            }

            if (!context.RefGender.Any())
            {
                context.RefTraumaLevel.Add(new RefTraumaLevel { Code = "F", Description = "FULL"});
                context.RefTraumaLevel.Add(new RefTraumaLevel { Code = "P", Description = "PARTIAL"});
                context.RefTraumaLevel.Add(new RefTraumaLevel { Code = "C", Description = "CONSULT"});

                context.SaveChanges();
            }
            LoadTestData(context);
        }

        private static void LoadTestData(Context context)
        {
            var patient1 = new Patient { FirstName = "Natasha", LastName = "Romanoff", MI = "L", DOB = new DateTime(1984, 6, 28) };
            context.Patients.Add(patient1);
            
            patient1.Events.Add(new Event { InjuryDateTime = DateTime.Now.AddYears(-2) });
 
            context.Patients.Add(new Patient { FirstName = "Steve", LastName = "Rogers", MI = "J", DOB = new DateTime(1918, 7, 4) });
            context.Patients.Add(new Patient { FirstName = "Carol", LastName = "Danvers", MI = "H", DOB = new DateTime(1961, 2, 12) });
            context.Patients.Add(new Patient { FirstName = "Tony", LastName = "Stark", MI = "", DOB = new DateTime(1970, 5, 29) });
            context.Patients.Add(new Patient { FirstName = "Wanda", LastName = "Maximoff", MI = "I", DOB = new DateTime(1999, 10, 14) });
            context.Patients.Add(new Patient { FirstName = "Nick", LastName = "Fury", MI = "V", DOB = new DateTime(1951, 12, 21) });
            context.Patients.Add(new Patient { FirstName = "Hope", LastName = "Van Dyne", MI = "", DOB = new DateTime(1982, 10, 2) });

            context.SaveChanges();



        }
    }
}
