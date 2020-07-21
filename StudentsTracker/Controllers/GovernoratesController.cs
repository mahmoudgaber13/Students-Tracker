using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentsTracker;

namespace StudentsTracker.Controllers
{
    public class GovernoratesController : Controller
    {
        private ArmyTechTaskEntities db = new ArmyTechTaskEntities();

        // GET: Governorates
        public async Task<ActionResult> Index()
        {
            return View(await db.Governorates.ToListAsync());
        }

        // GET: Governorates/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Governorate governorate = await db.Governorates.FindAsync(id);
            if (governorate == null)
            {
                return HttpNotFound();
            }
            return View(governorate);
        }

        // GET: Governorates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Governorates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name")] Governorate governorate)
        {
            if (ModelState.IsValid)
            {
                db.Governorates.Add(governorate);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(governorate);
        }

        // GET: Governorates/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Governorate governorate = await db.Governorates.FindAsync(id);
            if (governorate == null)
            {
                return HttpNotFound();
            }
            return View(governorate);
        }

        // POST: Governorates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name")] Governorate governorate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(governorate).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(governorate);
        }

        // GET: Governorates/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Governorate governorate = await db.Governorates.FindAsync(id);
            if (governorate == null)
            {
                return HttpNotFound();
            }
            return View(governorate);
        }

        // POST: Governorates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Governorate governorate = await db.Governorates.FindAsync(id);
            db.Governorates.Remove(governorate);
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
