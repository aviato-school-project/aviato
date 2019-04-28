using Aviato.Models;
using Aviato.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace Aviato.Controllers
{
    [HandleError]
    [Authorize]
    public class HomeController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();
        UserManager<IdentityUser> UserManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
        RoleManager<IdentityRole> RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());

        public async Task<ActionResult> Index()
        {
            // Vraća User Id trenutno ulogovanog usera
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            // Vraća rolu trenutno ulogovanog usera
            var role = await RoleManager.FindByIdAsync(user.Roles.First().RoleId);

            ViewBag.Email = user.Email;
            ViewBag.Rola = role.Name;

            EditUsersViewModel ezvm = new EditUsersViewModel();

            // Vraća zaposlenog koji je trenutno ulogovan
            ezvm.Zaposleni = db.Zaposleni.Where(z => z.IdentityId == user.Id).Select(z => z).First();
            int id = ezvm.Zaposleni.ZaposleniId;

            if (role.Name == "Pilot")
            {
                // Trenutno ulogovani pilot
                ezvm.Pilot = db.Pilot.Where(p => p.SifraPilota == id).Select(p => p).First();
                // Letovi za trenutno ulogovanog pilota
                ezvm.Let = db.Let.Where(l => l.Pilot == id || l.Kopilot == id).Select(l => l).Where(l => l.VremePoletanja > DateTime.Now).OrderBy(l => l.VremePoletanja).ToList();
            }
            else if (role.Name == "Stjuard")
            {
                // Letovi za trenutno ulogovanog stjuarda
                ezvm.Let = db.Let.Where(l => l.Stjuard1 == id || l.Stjuard2 == id).Select(l => l).Where(l => l.VremePoletanja > DateTime.Now).OrderBy(l => l.VremePoletanja).ToList();
            }
            else if (role.Name == "Mehaničar")
            {
                // Trenutno ulogovani mehaničar
                ezvm.Mehanicar = db.Mehanicar.Where(m => m.MehanicarId == id).Select(m => m).ToList();
                var licence = ezvm.Mehanicar.Select(m => m.Licenca).ToList();
                // Neispravni avioni za koje trenutno ulogovani mehaničar ima licence
                ezvm.Avions = (from a in db.Avion join m in db.Mehanicar on a.TipAviona equals m.Tip.TipId
                               where a.ServisniStatus == true
                               select a
                               ).Distinct().ToList();
            }

            return View(ezvm);
        }
        // GET: Home/ServisAviona/5
        [Authorize(Roles = "Mehaničar, Admin")]
        public ActionResult ServisAviona(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avion avion = db.Avion.Find(id);
            if (avion == null)
            {
                return HttpNotFound();
            }
            return View(avion);
        }

        // POST: Home/ServisAviona/5
        [HttpPost]
        [Authorize(Roles = "Mehaničar, Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult ServisAviona(Avion avion)
        {
            Avion servis = db.Avion.FirstOrDefault(a => a.AvionId == avion.AvionId);
            if (servis == null)
            {
                return HttpNotFound();
            }
            servis.ServisniStatus = avion.ServisniStatus;
            if (ModelState.IsValid)
            {
                db.SaveChanges();
                if(Roles.IsUserInRole("Mehaničar"))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    // Da admina ne bi vraćao na home page
                    return RedirectToAction("Index", "Avion");
                }
            }
            else
            {
                return View(avion);
            }
        }


        public JsonResult SkupiLetove()
        {
            int id = TrenutnoUlogovaniZaposleni();

            db.Configuration.ProxyCreationEnabled = false;

            var letovi = db.Let.Where(l => l.Pilot == id || l.Kopilot == id || l.Stjuard1 == id || l.Stjuard2 == id ).Select(l => l).ToList();
            return new JsonResult { Data = letovi, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        protected int TrenutnoUlogovaniZaposleni()
        {
            // Vraća User Id trenutno ulogovanog usera
            var user = UserManager.FindById(User.Identity.GetUserId());
            // Vraća rolu trenutno ulogovanog usera
            var role = RoleManager.FindById(user.Roles.First().RoleId);

            // Vraća zaposlenog koji je trenutno ulogovan
            int id = db.Zaposleni.Where(z => z.IdentityId == user.Id).Select(z => z.ZaposleniId).First();

            return id;
        }
    }
}