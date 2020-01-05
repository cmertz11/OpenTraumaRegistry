using OpenTraumaRegistry.Data.Models;
using Microsoft.EntityFrameworkCore;


namespace OpenTraumaRegistry.Data
{
    public class Context : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Vitals> Vitals { get; set; }
        public DbSet<Injury> Injuries { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<Complication> Complications { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Flag> Flags { get; set; }
        public DbSet<FlagReminder> FlagReminders { get; set; }
        public DbSet<FlagType> FlagTypes { get; set; }
        public DbSet<Models.RefGender> RefGender { get; set; }
        public DbSet<RefRace> RefRace { get; set; }
        public DbSet<RefTraumaType> RefTraumaType { get; set; }
        public DbSet<RefProtectiveDevice> RefProtectiveDevice { get; set; }
        public DbSet<RefRiskData> RefRiskData { get; set; }
        public DbSet<RefLocation> RefLocation { get; set; }
        public DbSet<RefTransport> RefTransport { get; set; }
        public DbSet<RefArrivedFrom> RefArrivedFrom { get; set; }
        public DbSet<RefTraumaLevel> RefTraumaLevel { get; set; }
        public DbSet<RefAgency> RefAgency { get; set; }
        public DbSet<RefICD10> RefICD10Codes { get; set; }
        public DbSet<RefPhysician> RefPhysicians { get; set; }
        public DbSet<Consult> Consults { get; set; }
        public DbSet<RefOutcome> RefOutcome { get; set; }
        public DbSet<ReferenceTables> ReferenceTables { get; set; }
        public DbSet<SafetyDevices> SafetyDevices { get; set; }
        public DbSet<InjuryTypes> InjuryTypes { get; set; }
        public DbSet<RiskData> RiskData { get; set; }
        public DbSet<RefHomeResidence> RefHomeResidence { get; set; }
        public DbSet<HomeResidence> HomeResidences { get; set; }
        public DbSet<RefChildSpecificRestraint> RefChildSpecificRestraint { get; set; }
        public DbSet<RefTransportMode> RefTransportMode { get; set; }
        public DbSet<RefDrugScreen> RefDrugScreen { get; set; }
        public DbSet<DrugScreen> DrugScreens { get; set; }

        public DbSet<User> Users { get; set; }
        public Context() {}
        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Flag>()
                .HasOne(p => p.FlagType)
                .WithOne(b => b.Flag)
                .HasForeignKey<Flag>(u => u.FlagTypeId)
                .HasConstraintName("ForeignKey_Flag_FlagType");

            modelBuilder.Entity<Vitals>()
                .HasOne(p => p.Event)
                .WithMany(b => b.Vitals)
                .HasForeignKey(p => p.EventId)
                .HasConstraintName("ForeignKey_Event_Vitals");

            modelBuilder.Entity<DrugScreen>()
                .HasOne(p => p.Event) 
                .WithMany(b => b.DrugScreens)
                .HasForeignKey(p => p.EventId)
                .HasConstraintName("ForeignKey_Event_DrugScreens");

            modelBuilder.Entity<Consult>()
                .HasOne(p => p.Event)
                .WithMany(b => b.Consults)
                .HasForeignKey(p => p.EventId)
                .HasConstraintName("ForeignKey_Event_Consults");

            modelBuilder.Entity<DrugScreenSubstances>()
                .HasOne(p => p.DrugScreen)
                .WithMany(p => p.DrugScreenSubstances)
                .HasForeignKey(p => p.DrugScreenId)
                .HasConstraintName("ForeignKey_Event_DrugScreenSubstances");

            modelBuilder.Entity<RiskData>()
                .HasOne(p => p.Event)
                .WithMany(b => b.Risks)
                .HasForeignKey(p => p.EventId)
                .HasConstraintName("ForeignKey_Event_RiskData");

            modelBuilder.Entity<InjuryTypes>()
                .HasOne(p => p.Event)
                .WithMany(i => i.InjuryTypes)
                .HasForeignKey(p => p.EventId)
                .HasConstraintName("ForeignKey_Event_InjuryTypes");

            modelBuilder.Entity<SafetyDevices>()
                .HasOne(p => p.Event)
                .WithMany(b => b.SafetyDevices)
                .HasForeignKey(p => p.EventId)
                .HasConstraintName("ForeignKey_Event_SafetyDevices");

            modelBuilder.Entity<HomeResidence>()
                .HasOne(p => p.Event)
                .WithMany(b => b.HomeResidences)
                .HasForeignKey(p => p.EventId)
                .HasConstraintName("ForeignKey_Event_HomeResidence");

            modelBuilder.Entity<Injury>()
                .HasOne(p => p.Event)
                .WithMany(b => b.Injuries)
                .HasForeignKey(p => p.EventId)
                .HasConstraintName("ForeignKey_Event_Injuries");

            modelBuilder.Entity<Procedure>()
                .HasOne(p => p.Event)
                .WithMany(b => b.Procedures)
                .HasForeignKey(p => p.EventId)
                .HasConstraintName("ForeignKey_Event_Procedures");

            modelBuilder.Entity<Complication>()
                .HasOne(p => p.Event)
                .WithMany(b => b.Complications)
                .HasForeignKey(p => p.EventId)
                .HasConstraintName("ForeignKey_Event_Complications");

            modelBuilder.Entity<Event>()
                .HasOne(p => p.Patient)
                .WithMany(b => b.Events)
                .HasForeignKey(p => p.PatientId)
                .HasConstraintName("ForeignKey_Patient_Events");

            modelBuilder.Entity<Unit>()
                .HasOne(p => p.Complication)
                .WithOne(b => b.Unit)
                .HasForeignKey<Unit>(u => u.ComplicationId)
                .HasConstraintName("ForeignKey_Complication_Unit");

            modelBuilder.Entity<FlagReminder>()
                .HasOne(p => p.Flag)
                .WithMany(b => b.FlagReminders)
                .HasForeignKey(p => p.FlagId)
                .HasConstraintName("ForeignKey_Flag_FlagReminders");

            modelBuilder.Entity<RiskData>()
                .HasOne(r => r.RefRiskData)
                .WithOne(b => b.RiskData)
                .HasForeignKey<RiskData>(u => u.RefRiskDataId)
                .HasConstraintName("ForeignKey_RiskData_RefRiskData");
                
        }
    }
}