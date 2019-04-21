namespace Aviato.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Mehanicar")]
    public partial class Mehanicar
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MehanicarId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Licenca { get; set; }

        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Datum sticanja licence")]
        public DateTime DatumLicence { get; set; }

        public virtual Tip Tip { get; set; }

        public virtual Zaposleni Zaposleni { get; set; }
    }
}
