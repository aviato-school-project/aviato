using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Aviato.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
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
                .WithRequired(e => e.Jezici)
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
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Zaposleni>()
                .HasMany(e => e.Pilot)
                .WithRequired(e => e.Zaposleni)
                .HasForeignKey(e => e.SifraPilota)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Zaposleni>()
                .HasMany(e => e.Stjuard)
                .WithRequired(e => e.Zaposleni)
                .HasForeignKey(e => e.StjuardId);

            base.OnModelCreating(modelBuilder);

        }



        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public class ApplicationRole : IdentityRole
        {
            public ApplicationRole() : base() { }
            public ApplicationRole(string roleName) : base(roleName) { }
        }

        public System.Data.Entity.DbSet<Aviato.ViewModels.ZaposleniSRolom> ZaposleniSRoloms { get; set; }

        public System.Data.Entity.DbSet<Aviato.ViewModels.EditUsersViewModel> EditUsersViewModels { get; set; }
    }
}