namespace Aviato.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Stjuard")]
    public partial class Stjuard
    {
        [Key]
        [Column(Order = 0)]
        [Display(Name = "Jezik")]
        public int JezikId { get; set; }

        [Key]
        [Column(Order = 1)]
        public int StjuardId { get; set; }

        //public int Id { get; set; }
        //[Display(Name = "Jezik")]
        public virtual Jezik Jezik { get; set; }

        public virtual Zaposleni Zaposleni { get; set; }
    }
}
