using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Aviato.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Aviato.Controllers
{
    public class LetController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        UserManager<IdentityUser> UserManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
        RoleManager<IdentityRole> RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());

        // GET: Let
        public async Task<ActionResult> Index()
        {
            var let = db.Let.Where(l => l.VremePoletanja > DateTime.Now).Include(l => l.Avion1).Include(l => l.Destinacija1).Include(l => l.Zaposleni).Include(l => l.Zaposleni1).Include(l => l.Zaposleni2).Include(l => l.Zaposleni3).OrderBy(l => l.VremePoletanja);
            var obavljeniLetovi = db.Let.Where(l => l.VremePoletanja < DateTime.Now).Include(l => l.Avion1).Include(l => l.Destinacija1).Include(l => l.Zaposleni).Include(l => l.Zaposleni1).Include(l => l.Zaposleni2).Include(l => l.Zaposleni3).OrderBy(l => l.VremePoletanja);
            return View(await let.ToListAsync());
        }

        // GET: Let/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Let let = await db.Let.FindAsync(id);
            if (let == null)
            {
                return HttpNotFound();
            }
            return View(let);
        }

        // GET: Let/Create
        public ActionResult Create()
        {
            ViewBag.Avion = new SelectList(db.Avion.Where(a => a.ServisniStatus == false), "AvionId", "SifraAviona");
            ViewBag.Destinacija = new SelectList(db.Destinacija, "DestinacijaId", "Naziv");

            List<Zaposleni> piloti = Zaposleni.ZaposleniPoRoli("Pilot");
            List<Zaposleni> stjuardi = Zaposleni.ZaposleniPoRoli("Stjuard");
            
            ViewBag.Pilot = new SelectList(from p in piloti select new { p.ZaposleniId, punoIme = p.Ime + " " + p.Prezime }, "ZaposleniId", "punoIme");
            ViewBag.Kopilot = new SelectList(from p in piloti select new { p.ZaposleniId, punoIme = p.Ime + " " + p.Prezime }, "ZaposleniId", "punoIme");
            ViewBag.Stjuard1 = new SelectList(from s in stjuardi select new { s.ZaposleniId, punoIme = s.Ime + " " + s.Prezime }, "ZaposleniId", "punoIme");
            ViewBag.Stjuard2 = new SelectList(from s in stjuardi select new { s.ZaposleniId, punoIme = s.Ime + " " + s.Prezime }, "ZaposleniId", "punoIme");

            return View();
        }

        // POST: Let/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "LetId,Destinacija,Avion,VremePoletanja,Pilot,Kopilot,Stjuard1,Stjuard2")] Let let)
        {
            if (ModelState.IsValid)
            {
                if (let.Stjuard1 == let.Stjuard2)
                {
                    ViewBag.Greska = "Stjuardi moraju biti različiti";
                    return RedirectToAction("Create");
                }
                if (let.Pilot == let.Kopilot)
                {
                    //return HttpNotFound();
                    ViewBag.Greska = "Piloti moraju biti različiti";
                    return RedirectToAction("Create");
                }

                // Izabrano vreme trenutnog leta
                var vreme = let.VremePoletanja;

                // Vraća listu id-ijeva svih destinacija na koje već imamo letove u isto vreme
                List<int> vecPutujemo = Let.ProveriDestinacie(vreme);

                // Vraća listu već zauzetih aviona
                List<int> vecZauzeti = Let.ProveriAvione(vreme);

                // Vraća listu id-ijeva svih zaposlenih koji su u isto vreme angažovani na drugim letovima
                List<int> vecAngazovani = Let.ProveriZaposlene(vreme);

                if (vecPutujemo.Contains(let.Destinacija))
                {
                    var naziv = db.Destinacija.Where(d => d.DestinacijaId == let.Destinacija).Select(z => z.Naziv).First().ToString();

                    ViewBag.Greska = $"Već imamo let za {naziv} u {vreme}";
                }
                else if (vecZauzeti.Contains(let.Avion))
                {
                    var naziv = db.Avion.Where(d => d.AvionId == let.Avion).Select(z => z.SifraAviona).First().ToString();

                    ViewBag.Greska = $"Avion {naziv} je već zauzet u {vreme}";
                }
                else if (vecAngazovani.Contains(let.Pilot))
                {
                    ViewBag.Greska = $"Pilot, {vratiIme(let.Pilot)}, je već angazovan na drugom letu";
                }
                else if (vecAngazovani.Contains(let.Kopilot))
                {
                    ViewBag.Greska = $"Kopilot, {vratiIme(let.Kopilot)}, je već angazovan na drugom letu";
                }
                else if (vecAngazovani.Contains(let.Stjuard1))
                {
                    ViewBag.Greska = $"Prvi stjuard, {vratiIme(let.Stjuard1)}, je već angazovan na drugom letu";
                }
                else if (vecAngazovani.Contains(let.Stjuard2))
                {
                    ViewBag.Greska = $"Drugi stjuard, {vratiIme(let.Stjuard2)}, je već angazovan na drugom letu";
                }
                else
                {
                    db.Let.Add(let);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
               
            }
            List<Zaposleni> piloti = Zaposleni.ZaposleniPoRoli("Pilot");
            List<Zaposleni> stjuardi = Zaposleni.ZaposleniPoRoli("Stjuard");

            ViewBag.Avion = new SelectList(db.Avion.Where(a => a.ServisniStatus == false), "AvionId", "SifraAviona", let.Avion);
            ViewBag.Destinacija = new SelectList(db.Destinacija, "DestinacijaId", "Naziv", let.Destinacija);
            ViewBag.Kopilot = new SelectList(from p in piloti select new { p.ZaposleniId, punoIme = p.Ime + " " + p.Prezime }, "ZaposleniId", "punoIme");
            ViewBag.Pilot = new SelectList(from p in piloti select new { p.ZaposleniId, punoIme = p.Ime + " " + p.Prezime }, "ZaposleniId", "punoIme");
            ViewBag.Stjuard1 = new SelectList(from s in stjuardi select new { s.ZaposleniId, punoIme = s.Ime + " " + s.Prezime }, "ZaposleniId", "punoIme");
            ViewBag.Stjuard2 = new SelectList(from s in stjuardi select new { s.ZaposleniId, punoIme = s.Ime + " " + s.Prezime }, "ZaposleniId", "punoIme");

            return View(let);
        }

        // GET: Let/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Let let = await db.Let.FindAsync(id);
            if (let == null)
            {
                return HttpNotFound();
            }

            if (let.Pilot == let.Kopilot || let.Stjuard1 == let.Stjuard2)
            {
                //return HttpNotFound();
                TempData["ispis"] = $"Pilot i stjuardi moraju biti razliciti";
                return RedirectToAction("Edit");
            }
            List<Zaposleni> piloti = Zaposleni.ZaposleniPoRoli("Pilot");
            List<Zaposleni> stjuardi = Zaposleni.ZaposleniPoRoli("Stjuard");

            /*
             * poslednji parametar ovde je vrednost iz same baze, 
             * koja je selektovana, u suprotnom se resetuje na
             * prvu vrednost dropdown-a:
             */

            ViewBag.Avion = new SelectList(db.Avion, "AvionId", "SifraAviona", let.Avion);
            ViewBag.Destinacija = new SelectList(db.Destinacija, "DestinacijaId", "Naziv", let.Destinacija);
                        
            ViewBag.Kopilot = new SelectList((from p in piloti select new { p.ZaposleniId, punoIme = p.Ime + " " + p.Prezime }), "ZaposleniId", "punoIme", let.Kopilot);
            ViewBag.Pilot = new SelectList((from p in piloti select new { p.ZaposleniId, punoIme = p.Ime + " " + p.Prezime }), "ZaposleniId", "punoIme", let.Pilot);
            ViewBag.Stjuard1 = new SelectList((from s in stjuardi select new { s.ZaposleniId, punoIme = s.Ime + " " + s.Prezime }), "ZaposleniId", "punoIme", let.Stjuard1);
            ViewBag.Stjuard2 = new SelectList((from s in stjuardi select new { s.ZaposleniId, punoIme = s.Ime + " " + s.Prezime }), "ZaposleniId", "punoIme", let.Stjuard2);
            return View(let);
        }

        // POST: Let/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "LetId,Destinacija,Avion,VremePoletanja,Pilot,Kopilot,Stjuard1,Stjuard2")] Let let)
        {
                if (ModelState.IsValid)
                {
                    db.Entry(let).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                /*
                 * U slučaju pogrešnog unosa strana se osvežava, da ne bi resetovala polja,
                 * ovde se čuvaju odabarne vrednosti
                 */

            List<Zaposleni> piloti = Zaposleni.ZaposleniPoRoli("Pilot");
            List<Zaposleni> stjuardi = Zaposleni.ZaposleniPoRoli("Stjuard");
            ViewBag.Avion = new SelectList(db.Avion, "AvionId", "SifraAviona");
            ViewBag.Destinacija = new SelectList(db.Destinacija, "DestinacijaId", "Naziv");
            ViewBag.Kopilot = new SelectList((from p in piloti select new { p.ZaposleniId, punoIme = p.Ime + " " + p.Prezime }), "ZaposleniId", "punoIme", let.Kopilot);
            ViewBag.Pilot = new SelectList((from p in piloti select new { p.ZaposleniId, punoIme = p.Ime + " " + p.Prezime }), "ZaposleniId", "punoIme", let.Pilot);
            ViewBag.Stjuard1 = new SelectList((from s in stjuardi select new { s.ZaposleniId, punoIme = s.Ime + " " + s.Prezime }), "ZaposleniId", "punoIme", let.Stjuard1);
            ViewBag.Stjuard2 = new SelectList((from s in stjuardi select new { s.ZaposleniId, punoIme = s.Ime + " " + s.Prezime }), "ZaposleniId", "punoIme", let.Stjuard2);

            return View(let);
        }

        // GET: Let/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Let let = await db.Let.FindAsync(id);
            if (let == null)
            {
                return HttpNotFound();
            }
            return View(let);
        }

        // POST: Let/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Let let = await db.Let.FindAsync(id);
            db.Let.Remove(let);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        protected string vratiIme(int id)
        {
            var ime = db.Zaposleni.Where(z => z.ZaposleniId == id).Select(z => z.Ime).First().ToString() + ' ' +
                      db.Zaposleni.Where(z => z.ZaposleniId == id).Select(z => z.Prezime).First().ToString();

            return ime;
        }
    }
}
