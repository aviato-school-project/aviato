using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aviato.ViewModels
{
    public class ZaposleniSRolom
    {
        [Key]
        public int ZSRId { get; set; }

        [Display(Name = "Ime i prezime")]
        public string ImeIPrezime { get; set; }
        //public string Prezime { get; set; }
        public string Email { get; set; }
        public string Pozicija { get; set; } // Rola
        public int ZaposleniId { get; set; }
        public string JMBG { get; set; }
        public int GodinaRođenja { get; set; }
    }
}