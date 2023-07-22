using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Pazzo.Repository.Models;

#nullable disable

namespace Pazzo.Repository.Contexts
{
    public partial class PazzoContext : DbContext
    {
        public PazzoContext()
        {
        }

        public PazzoContext(DbContextOptions<PazzoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Member> Members { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Member>(entity =>
            {
                entity.Property(e => e.IdNumber).IsFixedLength(true);

                entity.Property(e => e.Name).IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
