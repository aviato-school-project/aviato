namespace Aviato.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Pilot")]
    public partial class Pilot
    {
        public int PilotId { get; set; }
        [Display(Name = "Poslednji lekarski pregled")]
        [Column(TypeName = "smalldatetime")]
        public DateTime PoslednjiMedicinski { get; set; }

        [Display(Name = "Ocena zdravstvenog stanja")]
        public bool OcenaZS { get; set; }
        [Display(Name = "Sati letenja")]
        public int SatiLetenja { get; set; }
        [Display(Name = "Šifra pilota")]
        public int SifraPilota { get; set; }

        public virtual Zaposleni Zaposleni { get; set; }
    }
}
