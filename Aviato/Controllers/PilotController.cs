using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Aviato.Models;

namespace Aviato.Controllers
{
    public class PilotController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Pilot
        public async Task<ActionResult> Index()
        {
            var pilot = db.Pilot.Include(p => p.Zaposleni);
            return View(await pilot.ToListAsync());
        }

        // GET: Pilot/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pilot pilot = await db.Pilot.FindAsync(id);
            if (pilot == null)
            {
                return HttpNotFound();
            }
            return View(pilot);
        }

        // GET: Pilot/Create
        public ActionResult Create()
        {
            ViewBag.SifraPilota = new SelectList(db.Zaposleni, "ZaposleniId", "JMBG");
            return PartialView("~/Views/Pilot/Create.cshtml");
        }

        //// POST: Pilot/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "PilotId,PoslednjiMedicinski,OcenaZS,SatiLetenja,SifraPilota")] Pilot pilot)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Pilot.Add(pilot);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.SifraPilota = new SelectList(db.Zaposleni, "ZaposleniId", "JMBG", pilot.SifraPilota);
        //    return View(pilot);
        //}

        // GET: Pilot/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pilot pilot = await db.Pilot.FindAsync(id);
            if (pilot == null)
            {
                return HttpNotFound();
            }
            ViewBag.SifraPilota = new SelectList(db.Zaposleni, "ZaposleniId", "JMBG", pilot.SifraPilota);
            return View(pilot);
        }

        // POST: Pilot/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PilotId,PoslednjiMedicinski,OcenaZS,SatiLetenja,SifraPilota")] Pilot pilot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pilot).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SifraPilota = new SelectList(db.Zaposleni, "ZaposleniId", "JMBG", pilot.SifraPilota);
            return View(pilot);
        }

        // GET: Pilot/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pilot pilot = await db.Pilot.FindAsync(id);
            if (pilot == null)
            {
                return HttpNotFound();
            }
            return View(pilot);
        }

        // POST: Pilot/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Pilot pilot = await db.Pilot.FindAsync(id);
            db.Pilot.Remove(pilot);
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
    }
}
