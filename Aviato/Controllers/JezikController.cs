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
    public class JezikController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Jezik
        public async Task<ActionResult> Index()
        {
            return View(await db.Jezik.ToListAsync());
        }

        // GET: Jezik/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jezik jezik = await db.Jezik.FindAsync(id);
            if (jezik == null)
            {
                return HttpNotFound();
            }
            return View(jezik);
        }

        // GET: Jezik/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jezik/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "JezikId,Jezici")] Jezik jezik)
        {
            if (ModelState.IsValid)
            {
                db.Jezik.Add(jezik);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(jezik);
        }

        // GET: Jezik/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jezik jezik = await db.Jezik.FindAsync(id);
            if (jezik == null)
            {
                return HttpNotFound();
            }
            return View(jezik);
        }

        // POST: Jezik/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "JezikId,Jezici")] Jezik jezik)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jezik).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(jezik);
        }

        // GET: Jezik/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jezik jezik = await db.Jezik.FindAsync(id);
            if (jezik == null)
            {
                return HttpNotFound();
            }
            return View(jezik);
        }

        // POST: Jezik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Jezik jezik = await db.Jezik.FindAsync(id);
            db.Jezik.Remove(jezik);
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
