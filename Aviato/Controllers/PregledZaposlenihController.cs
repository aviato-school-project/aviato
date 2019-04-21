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
using Aviato.ViewModels;

namespace Aviato.Controllers
{
    public class PregledZaposlenihController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PregledZaposlenih
        public  ActionResult Index()
        {
            var pregled = new ZaposleniViewModel();
            return View(pregled);
        }

        // GET: PregledZaposlenih/Details/5
        //public async Task<ActionResult> Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ZaposleniViewModel zaposleniViewModel = await db.ZaposleniViewModels.FindAsync(id);
        //    if (zaposleniViewModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(zaposleniViewModel);
        //}

        // GET: PregledZaposlenih/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: PregledZaposlenih/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "Id,Ime,Prezime,JMBG,GodinaRodjenja,Email,Password,Role")] ZaposleniViewModel zaposleniViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.ZaposleniViewModels.Add(zaposleniViewModel);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    return View(zaposleniViewModel);
        //}

        // GET: PregledZaposlenih/Edit/5
        //public async Task<ActionResult> Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ZaposleniViewModel zaposleniViewModel = await db.ZaposleniViewModels.FindAsync(id);
        //    if (zaposleniViewModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(zaposleniViewModel);
        //}

        // POST: PregledZaposlenih/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "Id,Ime,Prezime,JMBG,GodinaRodjenja,Email,Password,Role")] ZaposleniViewModel zaposleniViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(zaposleniViewModel).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(zaposleniViewModel);
        //}

        // GET: PregledZaposlenih/Delete/5
        //public async Task<ActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ZaposleniViewModel zaposleniViewModel = await db.ZaposleniViewModels.FindAsync(id);
        //    if (zaposleniViewModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(zaposleniViewModel);
        //}

        //// POST: PregledZaposlenih/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(string id)
        //{
        //    ZaposleniViewModel zaposleniViewModel = await db.ZaposleniViewModels.FindAsync(id);
        //    db.ZaposleniViewModels.Remove(zaposleniViewModel);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
