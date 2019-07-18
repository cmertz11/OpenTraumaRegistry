﻿using TraumaRegistry.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace TraumaRegistry.Data
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
        public DbSet<RefInjuryType> RefInjuryType { get; set; }
        public DbSet<RefSafetyDevices> RefSafetyDevices { get; set; }
        public DbSet<RefRiskData> RefRiskData { get; set; }
        public DbSet<RefLocation> RefLocation { get; set; }
        public DbSet<RefTransport> RefTransport { get; set; }
        public DbSet<RefArrivedFrom> RefArrivedFrom { get; set; }
        public DbSet<RefTraumaLevel> RefTraumaLevel { get; set; }
        public Context() {}
        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Vitals>()
                .HasOne(p => p.Event)
                .WithMany(b => b.Vitals)
                .HasForeignKey(p => p.EventId)
                .HasConstraintName("ForeignKey_Event_Vitals");

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

            modelBuilder.Entity<Flag>()
                .HasOne(p => p.FlagType)
                .WithOne(b => b.Flag)
                .HasForeignKey<Flag>(u => u.FlagTypeId)
                .HasConstraintName("ForeignKey_Flag_FlagType");

            modelBuilder.Entity<FlagReminder>()
                .HasOne(p => p.Flag)
                .WithMany(b => b.FlagReminders)
                .HasForeignKey(p => p.FlagId)
                .HasConstraintName("ForeignKey_Flag_FlagReminders");

            modelBuilder.Entity<Log>()
                .Property(b => b.TimeStamp)
                .HasDefaultValueSql("getdate()") ;

            modelBuilder.Entity<Patient>()
                .Property(b => b.Created)
                .HasDefaultValueSql("getdate()");

 
                
                
        }
    }
}