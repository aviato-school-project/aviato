using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Aviato.Models;
using Aviato.ViewModels;

namespace Aviato.Controllers
{
    [HandleError]
    [Authorize(Roles = "Admin, SuperUser")]
    public class ZaposleniController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();

        // GET: Zaposleni
        public ActionResult Index()
        {
            var PrikaziZaposlene = (from U in db.Users
                                    join Z in db.Zaposleni
                                    on U.Id equals Z.IdentityId
                                    select new
                                    {
                                        ZaposleniId = Z.ZaposleniId,
                                        Ime = Z.Ime,
                                        Prezime = Z.Prezime,
                                        Email = U.Email,
                                        JMBG = Z.JMBG,
                                        GodinaRodjenja = Z.GodinaRodjenja,
                                        Pozicija = (from UR in U.Roles
                                                    join R in db.Roles
                                                    on UR.RoleId equals R.Id
                                                    select R.Name).ToList()
                                    }).ToList().Select(p => new Aviato.ViewModels.ZaposleniSRolom()
                                    {
                                        ZaposleniId = p.ZaposleniId,
                                        ImeIPrezime = p.Ime + ' ' + p.Prezime,
                                        Email = p.Email,
                                        JMBG = p.JMBG,
                                        GodinaRođenja = p.GodinaRodjenja,
                                        Pozicija = string.Join(",", p.Pozicija)
                                    }).OrderBy(p => p.Pozicija);
            return View(PrikaziZaposlene);
        }

        // GET: Zaposleni/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zaposleni zaposleni = await db.Zaposleni.FindAsync(id);
            var rola = "";
            try
            {
                rola = string.Join(", ", GetRoleByUserId(zaposleni.IdentityId));
                ViewBag.Pozicija = rola;
            }
            catch (Exception)
            {
                Server.ClearError();
                Response.Redirect("~/Error/BadRequest");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Los ili besmislen zahtev");
            }
            
            ViewBag.Email = (from U in db.Users
                             where U.Id == zaposleni.IdentityId
                             select U.Email).First();

            if (rola == "Pilot")
            {
                var pilot = (from p in db.Pilot
                             where p.SifraPilota == zaposleni.ZaposleniId
                             select p).First();
                ViewBag.Detalji = pilot;
            }
            else if (rola == "Stjuard")
            {
                var stjuard = (from s in db.Stjuard
                               join j in db.Jezik
                               on s.JezikId equals j.JezikId
                               where s.StjuardId == zaposleni.ZaposleniId
                               select j.Jezici).ToList();
                
                ViewBag.Detalji = stjuard;
            }
            else if (rola == "Mehaničar")
            {
                var mehanicar = (from m in db.Mehanicar
                                 join t in db.Tip
                                 on m.Licenca equals t.TipId
                                 where m.MehanicarId == zaposleni.ZaposleniId
                                 select m).ToList();

                ViewBag.Detalji = mehanicar;
            }

            if (zaposleni == null)
            {
                return HttpNotFound();
            }
            return View(zaposleni);
        }

        // GET: Zaposleni/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Zaposleni/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ZaposleniId,JMBG,Ime,Prezime,GodinaRodjenja,IdentityId")] Zaposleni zaposleni)
        {
            if (ModelState.IsValid)
            {
                db.Zaposleni.Add(zaposleni);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(zaposleni);
        }
        // GET: Zaposleni/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                //return Http
            }

            Zaposleni zaposleni = await db.Zaposleni.FindAsync(id);

            var rola = GetRoleByUserId(zaposleni.IdentityId);

            ViewBag.Pozicija = string.Join(", ", rola);
            EditUsersViewModel EZVM = new EditUsersViewModel()
            {
                Zaposleni = zaposleni
            };

            if (ViewBag.Pozicija == "Pilot")
            {
                Pilot pilotInfo = (from p in db.Pilot
                                   where p.SifraPilota == id
                                   select p).First();
                EZVM.Pilot = pilotInfo;
            }

            if (ViewBag.Pozicija == "Mehaničar")
            {
                ICollection<Mehanicar> mehanicariInfo = (from m in db.Mehanicar
                                                         where m.MehanicarId == id
                                                         select m).ToList();
                ViewBag.Licenca = new SelectList(db.Tip, "TipId", "NazivTipa");
                EZVM.Mehanicar = mehanicariInfo;
            }
            if (ViewBag.Pozicija == "Stjuard")
            {
                ICollection<Stjuard> stjuardiInfo = (from s in db.Stjuard
                                                     where s.StjuardId == id
                                                     select s).ToList();
                ViewBag.JezikId = new SelectList(db.Jezik, "JezikId", "Jezici");
                EZVM.Stjuard = stjuardiInfo;
            }

            if (zaposleni == null)
            {
                return HttpNotFound();
            }
            return View(EZVM);
        }

        // POST: Zaposleni/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ZaposleniId,JMBG,Ime,Prezime,GodinaRodjenja,IdentityId")] Zaposleni zaposleni,
                                             [Bind(Include = "PilotId,PoslednjiMedicinski,OcenaZS,SatiLetenja,SifraPilota")] Pilot pilot, 
                                              EditUsersViewModel EZVM)
        {
            if (ModelState.IsValid)
            {
                
                zaposleni.ZaposleniId = EZVM.Zaposleni.ZaposleniId;
                zaposleni.IdentityId = EZVM.Zaposleni.IdentityId;
                zaposleni.Ime = EZVM.Zaposleni.Ime;
                zaposleni.Prezime = EZVM.Zaposleni.Prezime;
                zaposleni.JMBG = EZVM.Zaposleni.JMBG;
                zaposleni.GodinaRodjenja = EZVM.Zaposleni.GodinaRodjenja;

                if(EZVM.Pilot != null)
                {
                    pilot.PilotId = EZVM.Pilot.PilotId;
                    pilot.PoslednjiMedicinski = EZVM.Pilot.PoslednjiMedicinski;
                    pilot.OcenaZS = EZVM.Pilot.OcenaZS;
                    pilot.SatiLetenja = EZVM.Pilot.SatiLetenja;
                    pilot.SifraPilota = EZVM.Zaposleni.ZaposleniId;
                    if (ModelState.IsValid)
                    {
                        db.Entry(pilot).State = EntityState.Modified;
                    }
                }

                if (EZVM.JeziciZaUnos != null)
                {
                    db.Stjuard.RemoveRange(db.Stjuard.Where(x => x.StjuardId == EZVM.Zaposleni.ZaposleniId));
                    await db.SaveChangesAsync();
                    
                    try
                    {
                        var jezici = EZVM.JeziciZaUnos.Split(',');
                        foreach (var j in jezici)
                        {
                            Stjuard stju = new Stjuard();
                            stju.StjuardId = EZVM.Zaposleni.ZaposleniId;
                            stju.JezikId = Convert.ToInt32(j);

                            db.Stjuard.Add(stju);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.Write(e);
                    }
                    await db.SaveChangesAsync();
                }

                else if (EZVM.tipoviZaUnos != null)
                {
                    db.Mehanicar.RemoveRange(db.Mehanicar.Where(x => x.MehanicarId == EZVM.Zaposleni.ZaposleniId));
                    await db.SaveChangesAsync();

                    var licence = EZVM.tipoviZaUnos.Split(',');
                    var datumi = EZVM.datumiZaUnos.Split(',');
                    for (var l = 0; l < licence.Length; l++)
                    {
                        Mehanicar meh = new Mehanicar();
                        meh.MehanicarId = EZVM.Zaposleni.ZaposleniId;
                        meh.Licenca = Convert.ToInt32(licence[l]);
                        meh.DatumLicence = Convert.ToDateTime(datumi[l]);

                        db.Mehanicar.Add(meh);
                    }
                    await db.SaveChangesAsync();
                }

                

                db.Entry(zaposleni).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(EZVM);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> EditPilota([Bind(Include = "ZaposleniId,JMBG,Ime,Prezime,GodinaRodjenja,IdentityId")] Zaposleni zaposleni, [Bind(Include = "PilotId,PoslednjiMedicinski,OcenaZS,SatiLetenja,SifraPilota")] Pilot pilot, EditUsersViewModel EZVM)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        zaposleni.ZaposleniId = EZVM.Zaposleni.ZaposleniId;
        //        zaposleni.IdentityId = EZVM.Zaposleni.IdentityId;
        //        zaposleni.Ime = EZVM.Zaposleni.Ime;
        //        zaposleni.Prezime = EZVM.Zaposleni.Prezime;
        //        zaposleni.JMBG = EZVM.Zaposleni.JMBG;
        //        zaposleni.GodinaRodjenja = EZVM.Zaposleni.GodinaRodjenja;

        //        pilot.PilotId = EZVM.Pilot.PilotId;
        //        pilot.PoslednjiMedicinski = EZVM.Pilot.PoslednjiMedicinski;
        //        pilot.OcenaZS = EZVM.Pilot.OcenaZS;
        //        pilot.SatiLetenja = EZVM.Pilot.SatiLetenja;
        //        pilot.SifraPilota = EZVM.Zaposleni.ZaposleniId;

        //        db.Entry(zaposleni).State = EntityState.Modified;
        //        db.Entry(pilot).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(EZVM);
        //}

        // GET: Zaposleni/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zaposleni zaposleni = await db.Zaposleni.FindAsync(id);
            if (zaposleni == null)
            {
                return HttpNotFound();
            }
            return View(zaposleni);
        }

        // POST: Zaposleni/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Zaposleni zaposleni = await db.Zaposleni.FindAsync(id);
            var UserId = zaposleni.IdentityId;
            var User = db.Users.Find(UserId);
            db.Users.Remove(User);
            //db.Zaposleni.Remove(zaposleni);
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

        #region Helpers
        public IEnumerable<string> GetRoleByUserId(string id)
        {
            var rola = (from U in db.Users
                        where U.Id == id
                        select new
                        {
                            Pozicija = (from UR in U.Roles
                                        join R in db.Roles
                                        on UR.RoleId equals R.Id
                                        select R.Name)
                        }).First();
            return rola.Pozicija;
        } 
        #endregion
    }
}
