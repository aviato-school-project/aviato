using Aviato.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace Aviato.ViewModels
{
    public class ZaposleniViewModel
    {
        public string Ime { get; set; } // Zaposleni
        public string Prezime { get; set; } // Zaposleni
        public string JMBG { get; set; } // Zaposleni
        public int GodinaRodjenja { get; set; } // Zaposleni

        [Key]
        public string Id { get; set; }

        [EmailAddress, Required]
        [Display(Name = "Email")]
        public string Email { get; set; } // AspNetUsers

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } // AspNetUsers

        public int Role { get; set; } // AspNetRoles

        

    }
}