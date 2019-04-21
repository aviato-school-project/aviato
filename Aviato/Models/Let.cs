namespace Aviato.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Let")]
    public partial class Let
    {
        public int LetId { get; set; }

        public int Destinacija { get; set; }

        public int Avion { get; set; }

        public DateTime VremePoletanja { get; set; }

        public int Pilot { get; set; }

        public int Kopilot { get; set; }

        public int Stjuard1 { get; set; }

        public int Stjuard2 { get; set; }

        public virtual Avion Avion1 { get; set; }

        public virtual Destinacija Destinacija1 { get; set; }

        public virtual Zaposleni Zaposleni { get; set; }

        public virtual Zaposleni Zaposleni1 { get; set; }

        public virtual Zaposleni Zaposleni2 { get; set; }

        public virtual Zaposleni Zaposleni3 { get; set; }
    }
}
