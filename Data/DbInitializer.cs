﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OpenTraumaRegistry.Data.Models;
using OpenTraumaRegistry.Shared;


namespace OpenTraumaRegistry.Data
{
    public static class DbInitializer
    {
        public static string SystemId { get; set; } = Guid.NewGuid().ToString();  //Get System Id from Identity 
        public static void Initialize(Context context, string UserEmail, string Password, string FirstName, string LastName, string Facilityname, IConfiguration configuration)
        {
            try
            {
                if(!context.Database.EnsureCreated())
                {
                   print ("The database was not created.  Check if a database with that name already exsist.");
                   return;
                }
                else
                {
                   print("Database Created Successfully.  Starting data load process...");
                   print("Starting data load process...");
                }
                if (!context.Users.Any())
                {
                    print("Loading User Account");
                    SecurityHelper passwordHasher = new SecurityHelper(configuration);
                    var systemAdmin = new User
                    {
                        Password = passwordHasher.Hash(Password),
                        EmailAddress = UserEmail,
                        FirstName = FirstName,
                        LastName = LastName,
                        EmailConfirmed = true, //TODO: Once email functionality is up, force email confirmation.
                        LastUpdate = DateTime.Now,
                        LastUpdatedBy = 0, // 0 UserID is System
                        PasswordExpires = DateTime.Now.AddDays(90), //TODO: This may need to be a config setting.
                        SystemAdministrator = true,
                        LoginAttempts = 0,
                        ForcePasswordReset = false,
                        Locked = false
                    };

                    context.Users.Add(systemAdmin);
                    context.SaveChanges();
                }
 
                if(!context.Facilities.Any())
                {
                    print("Loading Facility");
                    context.Facilities.Add(new Facility { FacilityName = Facilityname });
                    context.SaveChanges();
                }
                if(!context.UserFacilities.Any())
                {
                    print(string.Format("Attaching {0} to {1}", UserEmail, Facilityname));
                    context.UserFacilities.Add(new UserFacility { UserId = 1, FacilityId = 1, Administrator = true });
                }
                if (!context.RefSex.Any())
                {
                    print("Loading RefSex");
                    context.RefSex.Add(new Models.RefSex { Code = "M", Description = "Male" });
                    context.RefSex.Add(new Models.RefSex { Code = "F", Description = "Female" });
                    context.SaveChanges();
                }
                if (!context.RefRace.Any())
                {
                    print("Loading RefRace");
                    context.RefRace.Add(new RefRace { Code = "A", Description = "Asian" });
                    context.RefRace.Add(new RefRace { Code = "B", Description = "Native Hawaiian or Other Pacific Islander" });
                    context.RefRace.Add(new RefRace { Code = "H", Description = "Other Race" });
                    context.RefRace.Add(new RefRace { Code = "I", Description = "American Indian" });
                    context.RefRace.Add(new RefRace { Code = "O", Description = "Black or African American" });
                    context.RefRace.Add(new RefRace { Code = "W", Description = "White" });
                    context.SaveChanges();
                }
                if (!context.RefTraumaType.Any())
                {
                    print("Loading RefTraumaType"); 
                    context.RefTraumaType.Add(new RefTraumaType { Code = "", Description = "Blunt" });
                    context.RefTraumaType.Add(new RefTraumaType { Code = "", Description = "Penetrating" });
                    context.RefTraumaType.Add(new RefTraumaType { Code = "", Description = "Burn" });
                    context.RefTraumaType.Add(new RefTraumaType { Code = "", Description = "Other" });
                    context.SaveChanges();
                }
                
                if (!context.RefRiskData.Any())
                {
                    print("Loading RefRiskData");
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
                if (!context.RefProtectiveDevice.Any())
                {
                    print("Loading RefProtectiveDevice");
                    context.RefProtectiveDevice.Add(new RefProtectiveDevice { Description = "None" });
                    context.RefProtectiveDevice.Add(new RefProtectiveDevice { Description = "Lap belt" });
                    context.RefProtectiveDevice.Add(new RefProtectiveDevice { Description = "Personal Floatation Device" });
                    context.RefProtectiveDevice.Add(new RefProtectiveDevice { Description = "Protective Non-Clothing Gear (E.g. Shin Guard)" });
                    context.RefProtectiveDevice.Add(new RefProtectiveDevice { Description = "Eye protection" });
                    context.RefProtectiveDevice.Add(new RefProtectiveDevice { Description = "Child Restraint (Booster Seat or Child Car Seat)" });
                    context.RefProtectiveDevice.Add(new RefProtectiveDevice { Description = "Helmet (E.g. Bicycle, Skiing, Motorcycle)" });
                    context.RefProtectiveDevice.Add(new RefProtectiveDevice { Description = "Airbag Present" });
                    context.RefProtectiveDevice.Add(new RefProtectiveDevice { Description = "Protective Clothing (E.g. Padded Leather Pants)" });
                    context.RefProtectiveDevice.Add(new RefProtectiveDevice { Description = "Shoulder Belt" });
                    context.RefProtectiveDevice.Add(new RefProtectiveDevice { Description = "Other" });

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

                print("Loading RefHomeResidence");
                if (!context.RefHomeResidence.Any())
                {
                    context.RefHomeResidence.Add(new RefHomeResidence { Description = "Homeless" });
                    context.RefHomeResidence.Add(new RefHomeResidence { Description = "Undocumented Citizen" });
                    context.RefHomeResidence.Add(new RefHomeResidence { Description = "Migrant Worker" });

                    context.SaveChanges();
                }

                print("Loading RefTransportMode");
                if (!context.RefTransportMode.Any())
                {
                    context.RefTransportMode.Add(new RefTransportMode { Description = "Ground Ambulance" });
                    context.RefTransportMode.Add(new RefTransportMode { Description = "Helicopter Ambulance" });
                    context.RefTransportMode.Add(new RefTransportMode { Description = "Fixed-Wing Ambulance" });
                    context.RefTransportMode.Add(new RefTransportMode { Description = "Private/Public Vehicle/Walk-In" });
                    context.RefTransportMode.Add(new RefTransportMode { Description = "Police" });
                    context.RefTransportMode.Add(new RefTransportMode { Description = "Other" });
                    context.SaveChanges();
                }

                print("Loading RefEdDischargeDisposition");
                if (!context.RefEdDischargeDisposition.Any())
                {
                    context.RefEdDischargeDisposition.Add(new RefEdDischargeDisposition { Description = "Floor Bed (General Admission, Non-Specialty Unit Bed)" });
                    context.RefEdDischargeDisposition.Add(new RefEdDischargeDisposition { Description = "Observation Unit (Unit That Provides &lt; 24 Hour Stays)" });
                    context.RefEdDischargeDisposition.Add(new RefEdDischargeDisposition { Description = "Telemetry/Step-Down Unit (Less Acuity than ICU)" });
                    context.RefEdDischargeDisposition.Add(new RefEdDischargeDisposition { Description = "Home with Services" });
                    context.RefEdDischargeDisposition.Add(new RefEdDischargeDisposition { Description = "Deceased/Expired" });
                    context.RefEdDischargeDisposition.Add(new RefEdDischargeDisposition { Description = "Other (Jail, Institutional Care, Mental Health, Etc.)" });

                    context.RefEdDischargeDisposition.Add(new RefEdDischargeDisposition { Description = "Operating Room" });
                    context.RefEdDischargeDisposition.Add(new RefEdDischargeDisposition { Description = "Intensive Care Unit (ICU)" });
                    context.RefEdDischargeDisposition.Add(new RefEdDischargeDisposition { Description = "Home Without Services" });
                    context.RefEdDischargeDisposition.Add(new RefEdDischargeDisposition { Description = "Left Against Medical Advice" });
                    context.RefEdDischargeDisposition.Add(new RefEdDischargeDisposition { Description = "Transferred to Another Hospital" });
                    context.SaveChanges();
                }

                print("Loading RefOutcome");
                if (!context.RefOutcome.Any())
                {
                    context.RefOutcome.Add(new RefOutcome { Code = "A", Description = "Alive" });
                    context.RefOutcome.Add(new RefOutcome { Code = "D", Description = "Dead" });

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
                    context.RefPhysicians.Add(new RefPhysician { LastName = "Strange", FirstName = "Stephen", MI = "V" });
                    context.RefPhysicians.Add(new RefPhysician { LastName = "Robert", FirstName = "Banner", MI = "B" });

                    context.SaveChanges();
                }

                print("Loading RefChildSpecificRestraint");
                if (!context.RefChildSpecificRestraint.Any())
                {
                    context.RefChildSpecificRestraint.Add(new RefChildSpecificRestraint { Description = "Child Car Seat" });
                    context.RefChildSpecificRestraint.Add(new RefChildSpecificRestraint { Description = "Infant Car Seat" });
                    context.RefChildSpecificRestraint.Add(new RefChildSpecificRestraint { Description = "Child Booster Seat" });
                    context.SaveChanges();
                }

                print("Loading RefEthnicity");
                if (!context.RefEthnicity.Any())
                {
                    context.RefEthnicity.Add(new RefEthnicity { Description = "Hispanic or Latino" });
                    context.RefEthnicity.Add(new RefEthnicity { Description = "Not Hispanic or Latino" }); 
                    context.SaveChanges();
                }

                print("Loading RefAgeUnits");
                if (!context.RefAgeUnits.Any())
                {
                    context.RefAgeUnits.Add(new RefAgeUnits { Description = "Hours" });
                    context.RefAgeUnits.Add(new RefAgeUnits { Description = "Days" });
                    context.RefAgeUnits.Add(new RefAgeUnits { Description = "Months" });
                    context.RefAgeUnits.Add(new RefAgeUnits { Description = "Years" });
                    context.RefAgeUnits.Add(new RefAgeUnits { Description = "Minutes" });
                    context.RefAgeUnits.Add(new RefAgeUnits { Description = "Weeks" });
                    context.SaveChanges();
                }


                print("Loading RefPatientsOccupationalIndustry");
                if (!context.RefPatientsOccupationalIndustry.Any())
                {
                    context.RefPatientsOccupationalIndustry.Add(new RefPatientsOccupationalIndustry { Description = "Finance, Insurance, and Real Estate" });
                    context.RefPatientsOccupationalIndustry.Add(new RefPatientsOccupationalIndustry { Description = "Manufacturing" });
                    context.RefPatientsOccupationalIndustry.Add(new RefPatientsOccupationalIndustry { Description = "Retail Trade" });
                    context.RefPatientsOccupationalIndustry.Add(new RefPatientsOccupationalIndustry { Description = "Transportation and Public Utilities" });
                    context.RefPatientsOccupationalIndustry.Add(new RefPatientsOccupationalIndustry { Description = "Agriculture, Forestry, Fishing" });
                    context.RefPatientsOccupationalIndustry.Add(new RefPatientsOccupationalIndustry { Description = "Professional and Business Services" });
                    context.RefPatientsOccupationalIndustry.Add(new RefPatientsOccupationalIndustry { Description = "Education and Health Services" });
                    context.RefPatientsOccupationalIndustry.Add(new RefPatientsOccupationalIndustry { Description = "Construction" });
                    context.RefPatientsOccupationalIndustry.Add(new RefPatientsOccupationalIndustry { Description = "Government" });
                    context.RefPatientsOccupationalIndustry.Add(new RefPatientsOccupationalIndustry { Description = "Natural Resources and Mining" });
                    context.RefPatientsOccupationalIndustry.Add(new RefPatientsOccupationalIndustry { Description = "Information Services" });
                    context.RefPatientsOccupationalIndustry.Add(new RefPatientsOccupationalIndustry { Description = "Wholesale Trade" });
                    context.RefPatientsOccupationalIndustry.Add(new RefPatientsOccupationalIndustry { Description = "Leisure and Hospitality" });
                    context.RefPatientsOccupationalIndustry.Add(new RefPatientsOccupationalIndustry { Description = "Other Services" });
                   
                    context.SaveChanges();
                }

                print("Loading RefPatientsOccupationa");
                if (!context.RefPatientsOccupation.Any())
                {
                    context.RefPatientsOccupation.Add(new RefPatientsOccupation { Description = "Business and Financial Operations Occupations" });
                    context.RefPatientsOccupation.Add(new RefPatientsOccupation { Description = "Architecture and Engineering Occupations" });
                    context.RefPatientsOccupation.Add(new RefPatientsOccupation { Description = "Community and Social Services Occupations" });
                    context.RefPatientsOccupation.Add(new RefPatientsOccupation { Description = "Education, Training, and Library Occupations" });
                    context.RefPatientsOccupation.Add(new RefPatientsOccupation { Description = "Healthcare Practitioners and Technical Occupations" });
                    context.RefPatientsOccupation.Add(new RefPatientsOccupation { Description = "Protective Service Occupations" });
                    context.RefPatientsOccupation.Add(new RefPatientsOccupation { Description = "Building and Grounds Cleaning and Maintenance" });
                    context.RefPatientsOccupation.Add(new RefPatientsOccupation { Description = "Sales and Related Occupations" });
                    context.RefPatientsOccupation.Add(new RefPatientsOccupation { Description = "Farming, Fishing, and Forestry Occupations" });
                    context.RefPatientsOccupation.Add(new RefPatientsOccupation { Description = "Installation, Maintenance, and Repair Occupations" });
                    context.RefPatientsOccupation.Add(new RefPatientsOccupation { Description = "Transportation and Material Moving Occupations" });
                    context.RefPatientsOccupation.Add(new RefPatientsOccupation { Description = "Management Occupations" });
                    context.RefPatientsOccupation.Add(new RefPatientsOccupation { Description = "Computer and Mathematical Occupations" });
                    context.RefPatientsOccupation.Add(new RefPatientsOccupation { Description = "Life, Physical, and Social Science Occupations" }); 
                    context.RefPatientsOccupation.Add(new RefPatientsOccupation { Description = "Legal Occupations" });
                    context.RefPatientsOccupation.Add(new RefPatientsOccupation { Description = "Arts, Design, Entertainment, Sports, and Media" });
                    context.RefPatientsOccupation.Add(new RefPatientsOccupation { Description = "Healthcare Support Occupations" });
                    context.RefPatientsOccupation.Add(new RefPatientsOccupation { Description = "Food Preparation and Serving Related" });
                    context.RefPatientsOccupation.Add(new RefPatientsOccupation { Description = "Personal Care and Service Occupations" });
                    context.RefPatientsOccupation.Add(new RefPatientsOccupation { Description = "Office and Administrative Support Occupations" });
                    context.RefPatientsOccupation.Add(new RefPatientsOccupation { Description = "Construction and Extraction Occupations" });
                    context.RefPatientsOccupation.Add(new RefPatientsOccupation { Description = "Production Occupations" });
                    context.RefPatientsOccupation.Add(new RefPatientsOccupation { Description = "Military Specific Occupations" });

                }

                    print("Loading RefDrugScreen");
                if (!context.RefDrugScreen.Any())
                {
                    context.RefDrugScreen.Add(new RefDrugScreen {Code = "AMP", Description = "AMP (Amphetamine)" });
                    context.RefDrugScreen.Add(new RefDrugScreen {Code = "BAR", Description = "BAR (Barbiturate)" });
                    context.RefDrugScreen.Add(new RefDrugScreen {Code = "BZO", Description = "BZO (Benzodiazepines)" });
                    context.RefDrugScreen.Add(new RefDrugScreen {Code = "COC", Description = "COC (Cocaine)" });
                    context.RefDrugScreen.Add(new RefDrugScreen {Code = "mAMP", Description = "mAMP (Methamphetamine)" });
                    context.RefDrugScreen.Add(new RefDrugScreen {Code = "MDMA", Description = "MDMA (Ecstasy)" });
                    context.RefDrugScreen.Add(new RefDrugScreen {Code = "MTD", Description = "MTD (Methadone)" });
                    context.RefDrugScreen.Add(new RefDrugScreen {Code = "OPI", Description = "OPI (Opioid)" });
                    context.RefDrugScreen.Add(new RefDrugScreen {Code = "OXY", Description = "OXY (Oxycodone)" });
                    context.RefDrugScreen.Add(new RefDrugScreen {Code = "PCP", Description = "PCP (Phencyclidine)" });
                    context.RefDrugScreen.Add(new RefDrugScreen {Code = "TCA", Description = "TCA (Tricyclic Antidepressant)" });
                    context.RefDrugScreen.Add(new RefDrugScreen {Code = "THC", Description = "THC (Cannabinoid)" });
                    context.RefDrugScreen.Add(new RefDrugScreen {Code = "Other", Description = "Other" });
                    context.RefDrugScreen.Add(new RefDrugScreen {Code = "None", Description = "None" }); 
                    context.RefDrugScreen.Add(new RefDrugScreen {Code = "Not Tested", Description = "Not Tested" });
                    context.SaveChanges();
                }

                print("Loading Reference Table List with Id, Code, Description Schema");
                if (!context.ReferenceTables.Any())
                {
                    context.ReferenceTables.Add(new ReferenceTables { Code = "RefArrivedFrom", Description = "Arrived From" });
                    context.ReferenceTables.Add(new ReferenceTables { Code = "RefSex", Description = "Sex", Documentation = "The patient's sex." });
                    context.ReferenceTables.Add(new ReferenceTables { Code = "RefTraumaType", Description = "Trauma Types", Documentation = "The main type of trauma injury sustained by the patient (e.g. blunt, penetrating)." });
                    context.ReferenceTables.Add(new ReferenceTables { Code = "RefLocation", Description = "Locations" });
                    context.ReferenceTables.Add(new ReferenceTables { Code = "RefOutcome", Description = "Outcomes" });
                    context.ReferenceTables.Add(new ReferenceTables { Code = "RefRace", Description = "Race", Documentation= "The patient's race." });
                    context.ReferenceTables.Add(new ReferenceTables { Code = "RefRiskData", Description = "Risk Data" });
                    context.ReferenceTables.Add(new ReferenceTables { Code = "RefProtectiveDevice", Description = "Protective Device", Documentation = "Protective devices (safety equipment) in use or worn by the patient at the time of the injury." });
                    context.ReferenceTables.Add(new ReferenceTables { Code = "RefTransport", Description = "Transport" });
                    context.ReferenceTables.Add(new ReferenceTables { Code = "RefTraumaLevel", Description = "Trauma Level" });
                    context.ReferenceTables.Add(new ReferenceTables { Code = "ReferenceTables", Description = "Reference Tables" });
                    context.ReferenceTables.Add(new ReferenceTables { Code = "RefHomeResidence", Description = "Home Residence", Documentation= "Documentation of the type of patient without a home ZIP code." });
                    context.ReferenceTables.Add(new ReferenceTables { Code = "RefChildSpecificRestraint", Description = "Child Specific Restraints", Documentation = "Protective child restraint devices used by patient at the time of injury." });
                    context.ReferenceTables.Add(new ReferenceTables { Code = "RefDrugScreen", Description = "Drug Screen" });
                    context.ReferenceTables.Add(new ReferenceTables { Code = "RefEdDischargeDisposition", Description = "ED Discharge Disposition", Documentation = "The disposition of the patient at the time of discharge from the ED." });
                    context.ReferenceTables.Add(new ReferenceTables { Code = "RefPatientsOccupationalIndustry", Description = "Patients Occupational Industry", Documentation = "The occupational industry associated with the patient's work environment." });
                    context.ReferenceTables.Add(new ReferenceTables { Code = "RefPatientsOccupation", Description = "Patients Occupation", Documentation = "The occupation of the patient." });
                    context.ReferenceTables.Add(new ReferenceTables { Code = "RefEthnicity", Description = "Ethnicity", Documentation = "The patient's ethnicity." });
                    context.ReferenceTables.Add(new ReferenceTables { Code = "RefAgeUnits", Description = "Age Units", Documentation = "The units used to document the patient's age (Years, Months, Days, Hours)." });

                    context.SaveChanges();
                }

                print("Begin load of larger or advanced data sets.  This may take a while.");
                print("Loading ICD10 Codes");
                LoadICD10Codes(context);

                 LoadTestData(context);
                print("Database has been successfully Created.");
            }
            catch (Exception ex)
            {
                print(ex.ToString());
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

        private static void LoadTestData(Context context)
        { 
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
            for (int i = 0; i < 20; i++)
            {
                AddPatientRND(context, i);
                context.SaveChanges();
            }

        }

        private static void AddPatient1(Context context)
        {
            print("Adding Test Patient 1");
            var patient = new Patient { FirstName = "Natasha", LastName = "Romanoff", MI = "L", DOB = new DateTime(1984, 6, 28), GenderReferenceId = 2, RaceReferenceId = 6 , Created = System.DateTime.Now, LastUpdate = System.DateTime.Now, LastUpdatedBy = SystemId};
            patient.email = string.Format("{0}.{1}@gmail.com", patient.FirstName, patient.LastName);
            patient.MRN = GenerateMRN();
            print("Adding Test Event 1 for Patient 1");
            Event event1 = AddPatientEvent1();
            patient.Events.Add(event1);

            print("Adding Test Event 2 for Patient 1");
            Event event2 = AddPatientEvent2();
            patient.Events.Add(event2);

            context.Patients.Add(patient);
            context.SaveChanges();
        }


        private static void AddPatient2(Context context)
        {
            print("Adding Test Patient 2");
            var patient = new Patient { FirstName = "Steve", LastName = "Rogers", MI = "J", DOB = new DateTime(1918, 7, 4), GenderReferenceId = 1, RaceReferenceId = 6 };
            patient.email = string.Format("{0}.{1}@gmail.com", patient.FirstName, patient.LastName);
            patient.MRN = GenerateMRN();
            print("Adding Test Event 1 for Patient 2");
            Event event1 = AddPatientEvent1();

            patient.Events.Add(event1);
            context.Patients.Add(patient);
            context.SaveChanges();
        }

        private static void AddPatient3(Context context)
        {
            print("Adding Test Patient 3");
            var patient = new Patient { FirstName = "Carol", LastName = "Danvers", MI = "H", DOB = new DateTime(1961, 2, 12), GenderReferenceId = 2, RaceReferenceId = 6, Created = System.DateTime.Now, LastUpdate = System.DateTime.Now, LastUpdatedBy = SystemId };
            patient.email = string.Format("{0}.{1}@gmail.com", patient.FirstName, patient.LastName);
            patient.MRN = GenerateMRN();
            print("Adding Test Event 1 for Patient 3");
            Event event1 = AddPatientEvent1();

            patient.Events.Add(event1);
            context.Patients.Add(patient);
            context.SaveChanges();
        }

        private static void AddPatient4(Context context)
        {
            print("Adding Test Patient 4");
            var patient = new Patient { FirstName = "Tony", LastName = "Stark", MI = "", DOB = new DateTime(1970, 5, 29), GenderReferenceId = 1, RaceReferenceId = 6, Created = System.DateTime.Now, LastUpdate = System.DateTime.Now, LastUpdatedBy = SystemId };
            patient.email = string.Format("{0}.{1}@gmail.com", patient.FirstName, patient.LastName);
            patient.MRN = GenerateMRN();
            print("Adding Test Event 1 for Patient 4");
            Event event1 = AddPatientEvent1();

            patient.Events.Add(event1);
            context.Patients.Add(patient);
            context.SaveChanges();
        }

        private static void AddPatient5(Context context)
        {
            print("Adding Test Patient 5");
            var patient = new Patient { FirstName = "Wanda", LastName = "Maximoff", MI = "I", DOB = new DateTime(1999, 10, 14), GenderReferenceId = 2, RaceReferenceId = 6, Created = System.DateTime.Now, LastUpdate = System.DateTime.Now, LastUpdatedBy = SystemId };
            patient.email = string.Format("{0}.{1}@gmail.com", patient.FirstName, patient.LastName);
            patient.MRN = GenerateMRN();
            print("Adding Test Event 1 for Patient 5");
            Event event1 = AddPatientEvent1();

            patient.Events.Add(event1);
            context.Patients.Add(patient);
            context.SaveChanges();
        }

        private static void AddPatient6(Context context)
        {
            print("Adding Test Patient 6");
            var patient = new Patient { FirstName = "Hope", LastName = "Van Dyne", MI = "", DOB = new DateTime(1982, 10, 2), GenderReferenceId = 1, RaceReferenceId = 6, Created = System.DateTime.Now, LastUpdate = System.DateTime.Now, LastUpdatedBy = SystemId };
            patient.email = string.Format("{0}.{1}@gmail.com", patient.FirstName, patient.LastName);
            patient.MRN = GenerateMRN();
            print("Adding Test Event 1 for Patient 6");
            Event event1 = AddPatientEvent1();

            patient.Events.Add(event1);
            context.Patients.Add(patient);
            context.SaveChanges();
        }

        private static void AddPatient7(Context context)
        {
            print("Adding Test Patient 7");
            var patient = new Patient { FirstName = "Nick", LastName = "Fury", MI = "V", DOB = new DateTime(1951, 12, 21), GenderReferenceId = 1, RaceReferenceId = 1, Created = System.DateTime.Now, LastUpdate = System.DateTime.Now, LastUpdatedBy = SystemId };
            patient.email = string.Format("{0}.{1}@gmail.com", patient.FirstName, patient.LastName);
            patient.MRN = GenerateMRN();
            print("Adding Test Event 1 for Patient 7");
            Event event1 = AddPatientEvent1();

            patient.Events.Add(event1);
            context.Patients.Add(patient);
            context.SaveChanges();
        }

        private static void AddPatient8(Context context)
        {
            print("Adding Test Patient 8");
            var patient = new Patient { FirstName = "Samuel", LastName = "Wilson", MI = "T", DOB = new DateTime(1971, 12, 21), GenderReferenceId = 1, RaceReferenceId = 1, Created = System.DateTime.Now, LastUpdate = System.DateTime.Now, LastUpdatedBy = SystemId };
            patient.email = string.Format("{0}.{1}@gmail.com", patient.FirstName, patient.LastName);
            patient.MRN = GenerateMRN();
            print("Adding Test Event 1 for Patient 8");
            Event event1 = AddPatientEvent1();

            patient.Events.Add(event1);
            context.Patients.Add(patient);
            context.SaveChanges();
        }

        private static void AddPatient9(Context context)
        {
            print("Adding Test Patient 9");
            var patient = new Patient { FirstName = "Clint", LastName = "Barton", MI = "", DOB = new DateTime(1971, 12, 21), GenderReferenceId = 1, RaceReferenceId = 6, Created = System.DateTime.Now, LastUpdate = System.DateTime.Now, LastUpdatedBy = SystemId };
            patient.email = string.Format("{0}.{1}@gmail.com", patient.FirstName, patient.LastName);
            patient.MRN = GenerateMRN();
            print("Adding Test Event 1 for Patient 9");
            Event event1 = AddPatientEvent1();

            patient.Events.Add(event1);
            context.Patients.Add(patient);
            context.SaveChanges();
        }

        private static void AddPatient10(Context context)
        {
            print("Adding Test Patient 10");
            var patient = new Patient { FirstName = "James", LastName = "Rhodes", MI = "", DOB = new DateTime(1968, 12, 21), GenderReferenceId = 1, RaceReferenceId = 1, Created = System.DateTime.Now, LastUpdate = System.DateTime.Now, LastUpdatedBy = SystemId };
            patient.email = string.Format("{0}.{1}@gmail.com", patient.FirstName, patient.LastName);
            patient.MRN = GenerateMRN();
            print("Adding Test Event 1 for Patient 10");
            Event event1 = AddPatientEvent1();

            patient.Events.Add(event1);
            context.Patients.Add(patient);
            context.SaveChanges();
        }

        private static void AddPatientRND(Context context, int count)
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
            print("Adding Test Patient " + count);
            var patient = new Patient { FirstName = FirstName, LastName = LastName, MI = MI, DOB = dob, GenderReferenceId = f, RaceReferenceId = 1, Created = System.DateTime.Now, LastUpdate = System.DateTime.Now, LastUpdatedBy = SystemId };
            patient.email = string.Format("{0}.{1}@gmail.com", FirstName, LastName);
            patient.MRN = GenerateMRN();
            print("Adding Test Event 1 for Patient " + (count + 10).ToString()); // the + 10 is to account for the first 10 patients added.
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

            var injuryType1 = new InjuryTypes { RefTraumaTypeId = 2 };
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

            var injuryType1 = new InjuryTypes { RefTraumaTypeId = 2 };
            event1.InjuryTypes.Add(injuryType1);
            return event1;
        }

        private static void print(string message)
        {
            Console.WriteLine(System.DateTime.Now.ToString() + " - " + message);
        }
        private static string GenerateMRN()
        {
            Random r = new Random(System.DateTime.Now.Second);
            int rInt = r.Next(0, 100);
            return rInt.ToString().PadLeft(8, '0');
        }

    }
}
