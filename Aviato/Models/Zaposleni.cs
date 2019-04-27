namespace Aviato.Models
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Zaposleni")]
    public partial class Zaposleni
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Zaposleni()
        {
            Let = new HashSet<Let>();
            Let1 = new HashSet<Let>();
            Let2 = new HashSet<Let>();
            Let3 = new HashSet<Let>();
            Mehanicar = new HashSet<Mehanicar>();
            Pilot = new HashSet<Pilot>();
            Stjuard = new HashSet<Stjuard>();
        }

        public int ZaposleniId { get; set; }

        [Required]
        [StringLength(13)]
        public string JMBG { get; set; }

        [Required]
        [StringLength(20)]
        public string Ime { get; set; }

        [Required]
        [StringLength(20)]
        public string Prezime { get; set; }
        [Display(Name = "Godina rođenja")]
        public int GodinaRodjenja { get; set; }

        [StringLength(128)]
        public string IdentityId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Let> Let { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Let> Let1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Let> Let2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Let> Let3 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Mehanicar> Mehanicar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pilot> Pilot { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stjuard> Stjuard { get; set; }

        public static List<Zaposleni> ZaposleniPoRoli(string rola)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            UserManager<IdentityUser> UserManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
            var zaposleniId = db.Users.ToList().Where(x => UserManager.IsInRole(x.Id, rola)).ToList();
            List<Zaposleni> listaZaposlenih = new List<Zaposleni>();
            foreach (var zap in zaposleniId)
            {
                Zaposleni zaposleni = db.Zaposleni.Where(z => z.IdentityId == zap.Id).Select(z => z).FirstOrDefault();
                listaZaposlenih.Add(zaposleni);
            }

            return listaZaposlenih;
        }
    }
}
