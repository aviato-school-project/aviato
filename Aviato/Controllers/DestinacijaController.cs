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
    public class DestinacijaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Destinacija
        public async Task<ActionResult> Index()
        {
            var destinacija = db.Destinacija.Include(d => d.Jezici);
            return View(await destinacija.ToListAsync());
        }

        // GET: Destinacija/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Destinacija destinacija = await db.Destinacija.FindAsync(id);
            if (destinacija == null)
            {
                return HttpNotFound();
            }
            return View(destinacija);
        }

        // GET: Destinacija/Create
        public ActionResult Create()
        {
            ViewBag.Jezik = new SelectList(db.Jezik, "JezikId", "Jezici");
            return View();
        }

        // POST: Destinacija/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DestinacijaId,Naziv,TrajanjeLeta,Jezik")] Destinacija destinacija)
        {
            if (ModelState.IsValid)
            {
                db.Destinacija.Add(destinacija);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Jezik = new SelectList(db.Jezik, "JezikId", "Jezici", destinacija.Jezik);
            return View(destinacija);
        }

        // GET: Destinacija/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Destinacija destinacija = await db.Destinacija.FindAsync(id);
            if (destinacija == null)
            {
                return HttpNotFound();
            }
            ViewBag.Jezik = new SelectList(db.Jezik, "JezikId", "Jezici", destinacija.Jezik);
            return View(destinacija);
        }

        // POST: Destinacija/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DestinacijaId,Naziv,TrajanjeLeta,Jezik")] Destinacija destinacija)
        {
            if (ModelState.IsValid)
            {
                db.Entry(destinacija).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Jezik = new SelectList(db.Jezik, "JezikId", "Jezici", destinacija.Jezik);
            return View(destinacija);
        }

        // GET: Destinacija/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Destinacija destinacija = await db.Destinacija.FindAsync(id);
            if (destinacija == null)
            {
                return HttpNotFound();
            }
            return View(destinacija);
        }

        // POST: Destinacija/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Destinacija destinacija = await db.Destinacija.FindAsync(id);
            db.Destinacija.Remove(destinacija);
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
