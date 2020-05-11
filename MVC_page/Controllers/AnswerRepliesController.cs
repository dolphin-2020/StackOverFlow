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
    public class AnswerRepliesController : Controller
    {
        private DolphinQueryEntities db = new DolphinQueryEntities();

        // GET: AnswerReplies
        public ActionResult Index()
        {
            var answerReplies = db.AnswerReplies.Include(a => a.Answer).Include(a => a.User);
            return View(answerReplies.ToList());
        }

        // GET: AnswerReplies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnswerReply answerReply = db.AnswerReplies.Find(id);
            if (answerReply == null)
            {
                return HttpNotFound();
            }
            return View(answerReply);
        }

        // GET: AnswerReplies/Create
        public ActionResult Create()
        {
            ViewBag.answerId = new SelectList(db.Answers, "Id", "content");
            ViewBag.userId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: AnswerReplies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,content,userId,answerId,createTime")] AnswerReply answerReply)
        {
            if (ModelState.IsValid)
            {
                db.AnswerReplies.Add(answerReply);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.answerId = new SelectList(db.Answers, "Id", "content", answerReply.answerId);
            ViewBag.userId = new SelectList(db.Users, "Id", "Name", answerReply.userId);
            return View(answerReply);
        }

        // GET: AnswerReplies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnswerReply answerReply = db.AnswerReplies.Find(id);
            if (answerReply == null)
            {
                return HttpNotFound();
            }
            ViewBag.answerId = new SelectList(db.Answers, "Id", "content", answerReply.answerId);
            ViewBag.userId = new SelectList(db.Users, "Id", "Name", answerReply.userId);
            return View(answerReply);
        }

        // POST: AnswerReplies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,content,userId,answerId,createTime")] AnswerReply answerReply)
        {
            if (ModelState.IsValid)
            {
                db.Entry(answerReply).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.answerId = new SelectList(db.Answers, "Id", "content", answerReply.answerId);
            ViewBag.userId = new SelectList(db.Users, "Id", "Name", answerReply.userId);
            return View(answerReply);
        }

        // GET: AnswerReplies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnswerReply answerReply = db.AnswerReplies.Find(id);
            if (answerReply == null)
            {
                return HttpNotFound();
            }
            return View(answerReply);
        }

        // POST: AnswerReplies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AnswerReply answerReply = db.AnswerReplies.Find(id);
            db.AnswerReplies.Remove(answerReply);
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
