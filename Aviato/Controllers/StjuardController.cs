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
    public class StjuardController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Stjuard
        public async Task<ActionResult> Index()
        {
            var stjuard = db.Stjuard.Include(s => s.Jezik).Include(s => s.Zaposleni);
            return View(await stjuard.ToListAsync());
        }

        // GET: Stjuard/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stjuard stjuard = await db.Stjuard.FindAsync(id);
            if (stjuard == null)
            {
                return HttpNotFound();
            }
            return View(stjuard);
        }

        // GET: Stjuard/Create
        public ActionResult Create()
        {
            ViewBag.JezikId = new SelectList(db.Jezik, "JezikId", "Jezici");
            ViewBag.StjuardId = new SelectList(db.Zaposleni, "ZaposleniId", "JMBG");
            return PartialView("~/Views/Stjuard/Create.cshtml");
        }

        // POST: Stjuard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,JezikId,StjuardId")] Stjuard stjuard)
        {
            if (ModelState.IsValid)
            {
                db.Stjuard.Add(stjuard);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.JezikId = new SelectList(db.Jezik, "JezikId", "Jezici", stjuard.JezikId);
            ViewBag.StjuardId = new SelectList(db.Zaposleni, "ZaposleniId", "JMBG", stjuard.StjuardId);
            return View(stjuard);
        }

        // GET: Stjuard/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stjuard stjuard = await db.Stjuard.FindAsync(id);
            if (stjuard == null)
            {
                return HttpNotFound();
            }
            ViewBag.JezikId = new SelectList(db.Jezik, "JezikId", "Jezici", stjuard.JezikId);
            ViewBag.StjuardId = new SelectList(db.Zaposleni, "ZaposleniId", "JMBG", stjuard.StjuardId);
            return View(stjuard);
        }

        // POST: Stjuard/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,JezikId,StjuardId")] Stjuard stjuard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stjuard).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.JezikId = new SelectList(db.Jezik, "JezikId", "Jezici", stjuard.JezikId);
            ViewBag.StjuardId = new SelectList(db.Zaposleni, "ZaposleniId", "JMBG", stjuard.StjuardId);
            return View(stjuard);
        }

        // GET: Stjuard/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stjuard stjuard = await db.Stjuard.FindAsync(id);
            if (stjuard == null)
            {
                return HttpNotFound();
            }
            return View(stjuard);
        }

        // POST: Stjuard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Stjuard stjuard = await db.Stjuard.FindAsync(id);
            db.Stjuard.Remove(stjuard);
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
