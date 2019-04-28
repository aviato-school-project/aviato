namespace Aviato.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    [Table("Let")]
    public partial class Let
    {
        public int LetId { get; set; }
        public int Destinacija { get; set; }
        public int Avion { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm}")]
        public DateTime VremePoletanja { get; set; }
        [Display(Name = "Pilot")]
        public int Pilot { get; set; }
        public int Kopilot { get; set; }
        public int Stjuard1 { get; set; }
        //[DuplicateAttributes(ErrorMessage = "Ne možete jednog zaposlenog uneti dva puta po jednom letu")]
        public int Stjuard2 { get; set; }
        public virtual Avion Avion1 { get; set; }
        public virtual Destinacija Destinacija1 { get; set; }
        public virtual Zaposleni Zaposleni { get; set; }
        public virtual Zaposleni Zaposleni1 { get; set; }
        public virtual Zaposleni Zaposleni2 { get; set; }
        public virtual Zaposleni Zaposleni3 { get; set; }

        public static List<int> ProveriZaposlene(DateTime vreme)
        {
            List<Let> istovremeniLetovi = IstovremeniLetovi(vreme);

            // Lista id-ijeva svih zaposlenih koji su na istovremenim letovima
            List<int> vecAngazovani = new List<int>();
            foreach (var podudarni in istovremeniLetovi)
            {
                if (vecAngazovani.Count() == 0)
                {
                    vecAngazovani.Add(podudarni.Pilot);
                    vecAngazovani.Add(podudarni.Kopilot);
                    vecAngazovani.Add(podudarni.Stjuard1);
                    vecAngazovani.Add(podudarni.Stjuard2);
                }
                else if (!vecAngazovani.Contains(podudarni.Pilot))
                {
                    vecAngazovani.Add(podudarni.Pilot);
                }
                else if (!vecAngazovani.Contains(podudarni.Kopilot))
                {
                    vecAngazovani.Add(podudarni.Kopilot);
                }
                else if (!vecAngazovani.Contains(podudarni.Stjuard1))
                {
                    vecAngazovani.Add(podudarni.Stjuard1);
                }
                else if (!vecAngazovani.Contains(podudarni.Stjuard2))
                {
                    vecAngazovani.Add(podudarni.Stjuard2);
                }
            }
            
            return vecAngazovani;
        }

        public static List<int> ProveriAvione(DateTime vreme)
        {
            List<Let> istovremeniLetovi = IstovremeniLetovi(vreme);

            // Lista id-ijeva svih zaposlenih koji su na istovremenim letovima
            List<int> vecZauzeti = new List<int>();

            foreach (var podudarni in istovremeniLetovi)
            {
                if (vecZauzeti.Count() == 0)
                {
                    vecZauzeti.Add(podudarni.Avion);
                }
                else if (!vecZauzeti.Contains(podudarni.Avion))
                {
                    vecZauzeti.Add(podudarni.Pilot);
                }
            }

            return vecZauzeti;
        }
        public static List<int> ProveriDestinacie(DateTime vreme)
        {
            List<Let> istovremeniLetovi = IstovremeniLetovi(vreme);

            // Lista id-ijeva svih zaposlenih koji su na istovremenim letovima
            List<int> vecPutujemo = new List<int>();

            foreach (var podudarni in istovremeniLetovi)
            {
                if (vecPutujemo.Count() == 0)
                {
                    vecPutujemo.Add(podudarni.Destinacija);
                }
                else if (!vecPutujemo.Contains(podudarni.Destinacija))
                {
                    vecPutujemo.Add(podudarni.Pilot);
                }
            }

            return vecPutujemo;
        }

        public static List<Let> IstovremeniLetovi(DateTime vreme)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            // svi letovi koji su isto vreme
            List<Let> istovremeniLetovi = (from l in db.Let
                                           where l.VremePoletanja == vreme
                                           select l).ToList();

            return istovremeniLetovi;
        }
    }
}
