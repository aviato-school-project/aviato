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

        [Required]
        [StringLength(20)]
        public string Naziv { get; set; }

        public int TrajanjeLeta { get; set; }

        public int Jezik { get; set; }

        public virtual Jezik Jezici { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Let> Let { get; set; }
    }
}
