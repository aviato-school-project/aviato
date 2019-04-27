namespace Aviato.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Destinacija")]
    public partial class Destinacija
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Destinacija()
        {
            Let = new HashSet<Let>();
        }

        public int DestinacijaId { get; set; }

        [Required(ErrorMessage = "Unesite naziv destinacije")]
        [StringLength(20, ErrorMessage = "Destinacija ne može imati više od 20 karaktera")]
        [RegularExpression(@"^([a-zA-Z žćčšđŠĐČĆŽ]+)$", ErrorMessage = "Naziv destinacije može sadržati samo slova")]
        public string Naziv { get; set; }

        [Required(ErrorMessage = "Unesite trajanje leta")]
        [Display(Name = "Trajanje leta")]
        [RegularExpression(@"^([0-9]{1,2})", ErrorMessage = "Trajanje leta ne može biti duže od 2 cifre")]
        public int TrajanjeLeta { get; set; }

        public int Jezik { get; set; }

        public virtual Jezik Jezici { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Let> Let { get; set; }
    }
}
