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
    public class FieldsController : Controller
    {
        private ArmyTechTaskEntities db = new ArmyTechTaskEntities();

        // GET: Fields
        public async Task<ActionResult> Index()
        {
            return View(await db.Fields.ToListAsync());
        }

        // GET: Fields/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Field field = await db.Fields.FindAsync(id);
            if (field == null)
            {
                return HttpNotFound();
            }
            return View(field);
        }

        // GET: Fields/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fields/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name")] Field field)
        {
            if (ModelState.IsValid)
            {
                db.Fields.Add(field);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(field);
        }

        // GET: Fields/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Field field = await db.Fields.FindAsync(id);
            if (field == null)
            {
                return HttpNotFound();
            }
            return View(field);
        }

        // POST: Fields/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name")] Field field)
        {
            if (ModelState.IsValid)
            {
                db.Entry(field).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(field);
        }

        // GET: Fields/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Field field = await db.Fields.FindAsync(id);
            if (field == null)
            {
                return HttpNotFound();
            }
            return View(field);
        }

        // POST: Fields/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Field field = await db.Fields.FindAsync(id);
            db.Fields.Remove(field);
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
