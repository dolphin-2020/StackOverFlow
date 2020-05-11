using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_page;

namespace MVC_page.Controllers
{
    public class QuestionTypeAsksController : Controller
    {
        private DolphinQueryEntities db = new DolphinQueryEntities();

        // GET: QuestionTypeAsks
        public ActionResult Index()
        {
            var questionTypeAsks = db.QuestionTypeAsks.Include(q => q.Ask).Include(q => q.QuestionType);
            return View(questionTypeAsks.ToList());
        }

        // GET: QuestionTypeAsks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionTypeAsk questionTypeAsk = db.QuestionTypeAsks.Find(id);
            if (questionTypeAsk == null)
            {
                return HttpNotFound();
            }
            return View(questionTypeAsk);
        }

        // GET: QuestionTypeAsks/Create
        public ActionResult Create()
        {
            ViewBag.AskId = new SelectList(db.Asks, "Id", "title");
            ViewBag.QuestionTypeId = new SelectList(db.QuestionTypes, "Id", "Name");
            return View();
        }

        // POST: QuestionTypeAsks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,QuestionTypeId,AskId")] QuestionTypeAsk questionTypeAsk)
        {
            if (ModelState.IsValid)
            {
                db.QuestionTypeAsks.Add(questionTypeAsk);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AskId = new SelectList(db.Asks, "Id", "title", questionTypeAsk.AskId);
            ViewBag.QuestionTypeId = new SelectList(db.QuestionTypes, "Id", "Name", questionTypeAsk.QuestionTypeId);
            return View(questionTypeAsk);
        }

        // GET: QuestionTypeAsks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionTypeAsk questionTypeAsk = db.QuestionTypeAsks.Find(id);
            if (questionTypeAsk == null)
            {
                return HttpNotFound();
            }
            ViewBag.AskId = new SelectList(db.Asks, "Id", "title", questionTypeAsk.AskId);
            ViewBag.QuestionTypeId = new SelectList(db.QuestionTypes, "Id", "Name", questionTypeAsk.QuestionTypeId);
            return View(questionTypeAsk);
        }

        // POST: QuestionTypeAsks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,QuestionTypeId,AskId")] QuestionTypeAsk questionTypeAsk)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questionTypeAsk).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AskId = new SelectList(db.Asks, "Id", "title", questionTypeAsk.AskId);
            ViewBag.QuestionTypeId = new SelectList(db.QuestionTypes, "Id", "Name", questionTypeAsk.QuestionTypeId);
            return View(questionTypeAsk);
        }

        // GET: QuestionTypeAsks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionTypeAsk questionTypeAsk = db.QuestionTypeAsks.Find(id);
            if (questionTypeAsk == null)
            {
                return HttpNotFound();
            }
            return View(questionTypeAsk);
        }

        // POST: QuestionTypeAsks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuestionTypeAsk questionTypeAsk = db.QuestionTypeAsks.Find(id);
            db.QuestionTypeAsks.Remove(questionTypeAsk);
            db.SaveChanges();
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
