using System.Collections.Generic;
using System.Linq;
using TraumaData.Models;

namespace TraumaData
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
                new Reference{Name = "Location"}
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
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "A", Description = "", ReferenceId = race.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "B", Description = "", ReferenceId = race.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "H", Description = "", ReferenceId = race.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "I", Description = "", ReferenceId = race.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "O", Description = "", ReferenceId = race.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "W", Description = "", ReferenceId = race.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "P", Description = "", ReferenceId = race.Id });
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
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "", Description = "CHF", ReferenceId = riskData.Id });
            context.SaveChanges();
            var location = context.References.Where(r => r.Name == "Location").Single();
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "ED", Description = "Emergency Department", ReferenceId = location.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "ICU", Description = "Intensive Care Unit", ReferenceId = location.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "TB1", Description = "Trauma Bay 1", ReferenceId = location.Id });
            context.ReferenceDetails.Add(new ReferenceDetail { Code = "TB2", Description = "Trauma Bay 2", ReferenceId = location.Id });
            context.SaveChanges();
        }
    }
}
