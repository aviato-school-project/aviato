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
    public class EZVMController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EZVM
        //public async Task<ActionResult> Index()
        //{
        //    return View(await db.EditUsersViewModels.ToListAsync());
        //}

        // GET: EZVM/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    EditUsersViewModel editUsersViewModel = await db.EditUsersViewModels.FindAsync(id);
        //    if (editUsersViewModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(editUsersViewModel);
        //}

        // GET: EZVM/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: EZVM/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "Id")] EditUsersViewModel editUsersViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.EditUsersViewModels.Add(editUsersViewModel);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    return View(editUsersViewModel);
        //}

        // GET: EZVM/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    EditUsersViewModel editUsersViewModel = await db.EditUsersViewModels.FindAsync(id);
        //    if (editUsersViewModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(editUsersViewModel);
        //}

        // POST: EZVM/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "Id")] EditUsersViewModel editUsersViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(editUsersViewModel).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(editUsersViewModel);
        //}

        // GET: EZVM/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    EditUsersViewModel editUsersViewModel = await db.EditUsersViewModels.FindAsync(id);
        //    if (editUsersViewModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(editUsersViewModel);
        //}

        // POST: EZVM/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    EditUsersViewModel editUsersViewModel = await db.EditUsersViewModels.FindAsync(id);
        //    db.EditUsersViewModels.Remove(editUsersViewModel);
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
