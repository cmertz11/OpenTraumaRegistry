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

            if(context.References.Any())
            {
                return;
            }

            var references = new Reference[]
            {
                new Reference{Name = "Gender"},
                new Reference{Name = "Race"},
                new Reference{Name = "Injury Type"},
                new Reference{Name = "Safety Devices"},
                new Reference{Name = "Risk Data (Comorbids)"},
                new Reference{Name = "Location"},
                new Reference{Name = "Transport"},
                new Reference{Name = "Arrived From"},
                new Reference{Name = "Trauma Level"}
            };

            foreach (Reference r in references)
            { 
                context.Add(r); 
            }
            context.SaveChanges();

            var gender = context.References.Where(r => r.Name == "Gender").Single();
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "M", Description = "Male", ReferenceId = gender.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "F", Description = "Female", ReferenceId = gender.Id });
            context.SaveChanges();

            var race = context.References.Where(r => r.Name == "Race").Single();
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "A", Description = "Asian", ReferenceId = race.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "B", Description = "Black", ReferenceId = race.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "H", Description = "Hispanic", ReferenceId = race.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "I", Description = "Native American", ReferenceId = race.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "O", Description = "Other", ReferenceId = race.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "W", Description = "White", ReferenceId = race.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "P", Description = "Polinesian", ReferenceId = race.Id });
            context.SaveChanges();

            var injuryType = context.References.Where(r => r.Name == "Injury Type").Single();
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "", Description = "Blunt", ReferenceId = injuryType.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "", Description = "Penetrating", ReferenceId = injuryType.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "", Description = "Burn", ReferenceId = injuryType.Id });
            context.SaveChanges();

            var safetyDevices = context.References.Where(r => r.Name == "Safety Devices").Single();

            var riskData = context.References.Where(r => r.Name == "Risk Data (Comorbids)").Single();
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "", Description = "Advanced Directive LC", ReferenceId = riskData.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "", Description = "ADD_ADHD", ReferenceId = riskData.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "", Description = "ANGINA", ReferenceId = riskData.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "", Description = "ARREST", ReferenceId = riskData.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "", Description = "BLEEDING DISORDER", ReferenceId = riskData.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "", Description = "DISSEM CANCER", ReferenceId = riskData.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "", Description = "CHEMO", ReferenceId = riskData.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "", Description = "CIRRHOSIS", ReferenceId = riskData.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "", Description = "COPD", ReferenceId = riskData.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "", Description = "CHRONIC RENAL FAILURE", ReferenceId = riskData.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "", Description = "CVA W NEURO DEFICIT", ReferenceId = riskData.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "NTDB", Description = "DEMENTIA", ReferenceId = riskData.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "NTDB", Description = "DEP HEALTH STATUS", ReferenceId = riskData.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "", Description = "DIALYSIS", ReferenceId = riskData.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "NTDB", Description = "DIABETES", ReferenceId = riskData.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "", Description = "DRUG", ReferenceId = riskData.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "NTDB", Description = "ETOH", ReferenceId = riskData.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "NTDB", Description = "HTN", ReferenceId = riskData.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "", Description = "MI", ReferenceId = riskData.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "", Description = "PSYCH", ReferenceId = riskData.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "", Description = "PAD", ReferenceId = riskData.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "NTDB", Description = "SMOKER", ReferenceId = riskData.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "", Description = "STEROID", ReferenceId = riskData.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "", Description = "WARFARIN", ReferenceId = riskData.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "NTDB", Description = "ASA", ReferenceId = riskData.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "", Description = "PLAVIX", ReferenceId = riskData.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "", Description = "BETA", ReferenceId = riskData.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "", Description = "STATIN", ReferenceId = riskData.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "", Description = "DIRECT THROMBIN INHIBITOR", ReferenceId = riskData.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "", Description = "FACTOR XA INHIBITOR", ReferenceId = riskData.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "", Description = "ANTICOAGULATION THERAPY", ReferenceId = riskData.Id });

            context.SaveChanges();

            var location = context.References.Where(r => r.Name == "Location").Single();
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "ED", Description = "Emergency Department", ReferenceId = location.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "ICU", Description = "Intensive Care Unit", ReferenceId = location.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "TB1", Description = "Trauma Bay 1", ReferenceId = location.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "TB2", Description = "Trauma Bay 2", ReferenceId = location.Id });

            context.SaveChanges();

            var transport = context.References.Where(r => r.Name == "Transport").Single();
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "POV", Description = "Privately Owned vehicle", ReferenceId = transport.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "EMS", Description = "Emergency Medical Service", ReferenceId = transport.Id });

            context.SaveChanges();

            var arrivedFrom = context.References.Where(r => r.Name == "Arrived From").Single();
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "SCENE", Description = "SCENE", ReferenceId = arrivedFrom.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "HOME", Description = "HOME", ReferenceId = arrivedFrom.Id });

            context.SaveChanges();

            var traumaLevel = context.References.Where(r => r.Name == "Trauma Level").Single();
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "F", Description = "FULL", ReferenceId = traumaLevel.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "P", Description = "PARTIAL", ReferenceId = traumaLevel.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "C", Description = "CONSULT", ReferenceId = traumaLevel.Id });

            context.SaveChanges();


        }
    }
}
