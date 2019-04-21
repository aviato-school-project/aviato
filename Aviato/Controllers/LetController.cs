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
    public class LetController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Let
        public async Task<ActionResult> Index()
        {
            var let = db.Let.Include(l => l.Avion1).Include(l => l.Destinacija1).Include(l => l.Zaposleni).Include(l => l.Zaposleni1).Include(l => l.Zaposleni2).Include(l => l.Zaposleni3);
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
            ViewBag.Avion = new SelectList(db.Avion, "AvionId", "SifraAviona");
            ViewBag.Destinacija = new SelectList(db.Destinacija, "DestinacijaId", "Naziv");
            ViewBag.Kopilot = new SelectList(db.Zaposleni, "ZaposleniId", "JMBG");
            ViewBag.Pilot = new SelectList(db.Zaposleni, "ZaposleniId", "JMBG");
            ViewBag.Stjuard1 = new SelectList(db.Zaposleni, "ZaposleniId", "JMBG");
            ViewBag.Stjuard2 = new SelectList(db.Zaposleni, "ZaposleniId", "JMBG");
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
                db.Let.Add(let);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Avion = new SelectList(db.Avion, "AvionId", "SifraAviona", let.Avion);
            ViewBag.Destinacija = new SelectList(db.Destinacija, "DestinacijaId", "Naziv", let.Destinacija);
            ViewBag.Kopilot = new SelectList(db.Zaposleni, "ZaposleniId", "JMBG", let.Kopilot);
            ViewBag.Pilot = new SelectList(db.Zaposleni, "ZaposleniId", "JMBG", let.Pilot);
            ViewBag.Stjuard1 = new SelectList(db.Zaposleni, "ZaposleniId", "JMBG", let.Stjuard1);
            ViewBag.Stjuard2 = new SelectList(db.Zaposleni, "ZaposleniId", "JMBG", let.Stjuard2);
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
            ViewBag.Avion = new SelectList(db.Avion, "AvionId", "SifraAviona", let.Avion);
            ViewBag.Destinacija = new SelectList(db.Destinacija, "DestinacijaId", "Naziv", let.Destinacija);
            ViewBag.Kopilot = new SelectList(db.Zaposleni, "ZaposleniId", "JMBG", let.Kopilot);
            ViewBag.Pilot = new SelectList(db.Zaposleni, "ZaposleniId", "JMBG", let.Pilot);
            ViewBag.Stjuard1 = new SelectList(db.Zaposleni, "ZaposleniId", "JMBG", let.Stjuard1);
            ViewBag.Stjuard2 = new SelectList(db.Zaposleni, "ZaposleniId", "JMBG", let.Stjuard2);
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
            ViewBag.Avion = new SelectList(db.Avion, "AvionId", "SifraAviona", let.Avion);
            ViewBag.Destinacija = new SelectList(db.Destinacija, "DestinacijaId", "Naziv", let.Destinacija);
            ViewBag.Kopilot = new SelectList(db.Zaposleni, "ZaposleniId", "JMBG", let.Kopilot);
            ViewBag.Pilot = new SelectList(db.Zaposleni, "ZaposleniId", "JMBG", let.Pilot);
            ViewBag.Stjuard1 = new SelectList(db.Zaposleni, "ZaposleniId", "JMBG", let.Stjuard1);
            ViewBag.Stjuard2 = new SelectList(db.Zaposleni, "ZaposleniId", "JMBG", let.Stjuard2);
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
    }
}
