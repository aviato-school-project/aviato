namespace Aviato.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Aviato : DbContext
    {
        public Aviato()
            : base("name=Aviato")
        {
        }

        public virtual DbSet<Avion> Avion { get; set; }
        public virtual DbSet<Destinacija> Destinacija { get; set; }
        public virtual DbSet<Jezik> Jezik { get; set; }
        public virtual DbSet<Let> Let { get; set; }
        public virtual DbSet<Mehanicar> Mehanicar { get; set; }
        public virtual DbSet<Pilot> Pilot { get; set; }
        public virtual DbSet<Stjuard> Stjuard { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Tip> Tip { get; set; }
        public virtual DbSet<Zaposleni> Zaposleni { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Avion>()
                .HasMany(e => e.Let)
                .WithRequired(e => e.Avion1)
                .HasForeignKey(e => e.Avion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Destinacija>()
                .HasMany(e => e.Let)
                .WithRequired(e => e.Destinacija1)
                .HasForeignKey(e => e.Destinacija)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Jezik>()
                .HasMany(e => e.Destinacija)
                .WithRequired(e => e.Jezik1)
                .HasForeignKey(e => e.Jezik)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tip>()
                .HasMany(e => e.Avion)
                .WithRequired(e => e.Tip)
                .HasForeignKey(e => e.TipAviona)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tip>()
                .HasMany(e => e.Mehanicar)
                .WithRequired(e => e.Tip)
                .HasForeignKey(e => e.Licenca)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Zaposleni>()
                .HasMany(e => e.Let)
                .WithRequired(e => e.Zaposleni)
                .HasForeignKey(e => e.Kopilot)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Zaposleni>()
                .HasMany(e => e.Let1)
                .WithRequired(e => e.Zaposleni1)
                .HasForeignKey(e => e.Pilot)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Zaposleni>()
                .HasMany(e => e.Let2)
                .WithRequired(e => e.Zaposleni2)
                .HasForeignKey(e => e.Stjuard1)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Zaposleni>()
                .HasMany(e => e.Let3)
                .WithRequired(e => e.Zaposleni3)
                .HasForeignKey(e => e.Stjuard2)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Zaposleni>()
                .HasMany(e => e.Mehanicar)
                .WithRequired(e => e.Zaposleni)
                .HasForeignKey(e => e.MehanicarId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Zaposleni>()
                .HasMany(e => e.Pilot)
                .WithRequired(e => e.Zaposleni)
                .HasForeignKey(e => e.SifraPilota)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Zaposleni>()
                .HasMany(e => e.Stjuard)
                .WithRequired(e => e.Zaposleni)
                .HasForeignKey(e => e.StjuardId);
        }
    }
}
