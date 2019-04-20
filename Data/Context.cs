using TraumaData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TraumaData
{
    public class Context : DbContext
    {
        public DbSet<Reference> References { get; set; }
        public DbSet<ReferenceDetail> ReferenceDetails { get; set; }
        public Context(DbContextOptions<Context> options) : base(options) {}
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reference>().ToTable("Reference");
            modelBuilder.Entity<ReferenceDetail>().ToTable("ReferenceDetail");

            modelBuilder.Entity<ReferenceDetail>()
                .HasOne(p => p.Reference)
                .WithMany(b => b.ReferenceDetails)
                .HasForeignKey(p => p.ReferenceId)
                .HasConstraintName("ForeignKey_Reference_ReferenceData");
        }

    }
}
