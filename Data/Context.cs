using TraumaData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TraumaData
{
    public class Context : DbContext
    {
        public Context() { }
        public DbSet<Reference> References { get; set; }
        public DbSet<ReferenceDetail> ReferenceDetails { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Vitals> Vitals { get; set; }
        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reference>()
                .HasIndex(n => n.Name)
                .IsUnique();

            modelBuilder.Entity<Reference>().ToTable("Reference");
            modelBuilder.Entity<ReferenceDetail>().ToTable("ReferenceDetail");

            modelBuilder.Entity<ReferenceDetail>()
                .HasOne(p => p.Reference)
                .WithMany(b => b.ReferenceDetails)
                .HasForeignKey(p => p.ReferenceId)
                .HasConstraintName("ForeignKey_Reference_ReferenceData");


            modelBuilder.Entity<Log>()
            .Property(b => b.TimeStamp)
            .HasDefaultValueSql("getdate()");
        }

    }
}