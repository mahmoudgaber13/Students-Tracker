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
    public class StudentsController : Controller
    {
        private ArmyTechTaskEntities db = new ArmyTechTaskEntities();

        // GET: Students
        public async Task<ActionResult> Index()
        {
            var students = db.Students.Include(s => s.Field).Include(s => s.Governorate).Include(s => s.Neighborhood);
            return View(await students.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.FieldId = new SelectList(db.Fields, "ID", "Name");
            ViewBag.GovernorateId = new SelectList(db.Governorates, "ID", "Name");
            ViewBag.NeighborhoodId = new SelectList(db.Neighborhoods, "ID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,BirthDate,GovernorateId,NeighborhoodId,FieldId")] Student student)
        {
            bool NeighborhoodFlag = false;
            if(db.Governorates.Find(student.GovernorateId).Neighborhoods.Contains(db.Neighborhoods.Find(student.NeighborhoodId)))
            {
                NeighborhoodFlag = true;
            }
            else
            {
                ViewBag.NeighborhoodError = "Selected Neighborhood is not in selected Governorate";
            }
            if (ModelState.IsValid && NeighborhoodFlag)
            {
                
                
                return RedirectToAction("Save",student);
            }

            ViewBag.FieldId = new SelectList(db.Fields, "ID", "Name", student.FieldId);
            ViewBag.GovernorateId = new SelectList(db.Governorates, "ID", "Name", student.GovernorateId);
            ViewBag.NeighborhoodId = new SelectList(db.Neighborhoods, "ID", "Name", student.NeighborhoodId);
            return View(student);
        }

        public ActionResult Save(Student student)
        {
            if (student == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.Field = db.Fields.Find(student.FieldId).Name;
            ViewBag.Governorate = db.Governorates.Find(student.GovernorateId).Name;
            ViewBag.Neighborhood = db.Neighborhoods.Find(student.NeighborhoodId).Name;
            return View(student);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Save")]

        public async Task<ActionResult> SaveConfirmed(Student student)
        {
            db.Students.Add(student);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Students/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.FieldId = new SelectList(db.Fields, "ID", "Name", student.FieldId);
            ViewBag.GovernorateId = new SelectList(db.Governorates, "ID", "Name", student.GovernorateId);
            ViewBag.NeighborhoodId = new SelectList(db.Neighborhoods, "ID", "Name", student.NeighborhoodId);
            return View(student);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,BirthDate,GovernorateId,NeighborhoodId,FieldId")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FieldId = new SelectList(db.Fields, "ID", "Name", student.FieldId);
            ViewBag.GovernorateId = new SelectList(db.Governorates, "ID", "Name", student.GovernorateId);
            ViewBag.NeighborhoodId = new SelectList(db.Neighborhoods, "ID", "Name", student.NeighborhoodId);
            return View(student);
        }


       

        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Student student = await db.Students.FindAsync(id);
            db.Students.Remove(student);
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
