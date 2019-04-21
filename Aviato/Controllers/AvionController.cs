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
    public class AvionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Avion
        public async Task<ActionResult> Index()
        {
            var avion = db.Avion.Include(a => a.Tip);
            return View(await avion.ToListAsync());
        }

        // GET: Avion/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avion avion = await db.Avion.FindAsync(id);
            if (avion == null)
            {
                return HttpNotFound();
            }
            return View(avion);
        }

        // GET: Avion/Create
        public ActionResult Create()
        {
            ViewBag.TipAviona = new SelectList(db.Tip, "TipId", "NazivTipa");
            return View();
        }

        // POST: Avion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AvionId,SifraAviona,GodinaProizvodnje,ServisniStatus,TipAviona")] Avion avion)
        {
            if (ModelState.IsValid)
            {
                db.Avion.Add(avion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.TipAviona = new SelectList(db.Tip, "TipId", "NazivTipa", avion.TipAviona);
            return View(avion);
        }

        // GET: Avion/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avion avion = await db.Avion.FindAsync(id);
            if (avion == null)
            {
                return HttpNotFound();
            }
            ViewBag.TipAviona = new SelectList(db.Tip, "TipId", "NazivTipa", avion.TipAviona);
            return View(avion);
        }

        // POST: Avion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AvionId,SifraAviona,GodinaProizvodnje,ServisniStatus,TipAviona")] Avion avion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(avion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TipAviona = new SelectList(db.Tip, "TipId", "NazivTipa", avion.TipAviona);
            return View(avion);
        }

        // GET: Avion/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avion avion = await db.Avion.FindAsync(id);
            if (avion == null)
            {
                return HttpNotFound();
            }
            return View(avion);
        }

        // POST: Avion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Avion avion = await db.Avion.FindAsync(id);
            db.Avion.Remove(avion);
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
