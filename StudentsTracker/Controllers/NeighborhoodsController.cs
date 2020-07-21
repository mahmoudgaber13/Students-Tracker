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
    public class NeighborhoodsController : Controller
    {
        private ArmyTechTaskEntities db = new ArmyTechTaskEntities();

        // GET: Neighborhoods
        public async Task<ActionResult> Index()
        {
            var neighborhoods = db.Neighborhoods.Include(n => n.Governorate);
            return View(await neighborhoods.ToListAsync());
        }

        // GET: Neighborhoods/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Neighborhood neighborhood = await db.Neighborhoods.FindAsync(id);
            if (neighborhood == null)
            {
                return HttpNotFound();
            }
            return View(neighborhood);
        }

        // GET: Neighborhoods/Create
        public ActionResult Create()
        {
            ViewBag.GovernorateId = new SelectList(db.Governorates, "ID", "Name");
            return View();
        }

        // POST: Neighborhoods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,GovernorateId")] Neighborhood neighborhood)
        {
            if (ModelState.IsValid)
            {
                db.Neighborhoods.Add(neighborhood);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.GovernorateId = new SelectList(db.Governorates, "ID", "Name", neighborhood.GovernorateId);
            return View(neighborhood);
        }

        // GET: Neighborhoods/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Neighborhood neighborhood = await db.Neighborhoods.FindAsync(id);
            if (neighborhood == null)
            {
                return HttpNotFound();
            }
            ViewBag.GovernorateId = new SelectList(db.Governorates, "ID", "Name", neighborhood.GovernorateId);
            return View(neighborhood);
        }

        // POST: Neighborhoods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,GovernorateId")] Neighborhood neighborhood)
        {
            if (ModelState.IsValid)
            {
                db.Entry(neighborhood).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.GovernorateId = new SelectList(db.Governorates, "ID", "Name", neighborhood.GovernorateId);
            return View(neighborhood);
        }

        // GET: Neighborhoods/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Neighborhood neighborhood = await db.Neighborhoods.FindAsync(id);
            if (neighborhood == null)
            {
                return HttpNotFound();
            }
            return View(neighborhood);
        }

        // POST: Neighborhoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Neighborhood neighborhood = await db.Neighborhoods.FindAsync(id);
            db.Neighborhoods.Remove(neighborhood);
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
