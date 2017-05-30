using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrabFinal___PeRsH.Models;

namespace TrabFinal___PeRsH.Controllers
{
    public class ComentReportsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ComentReports
        public ActionResult Index()
        {
            var comentReports = db.ComentReports.Include(c => c.Comentarios).Include(c => c.User);
            return View(comentReports.ToList());
        }

        // GET: ComentReports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComentReports comentReports = db.ComentReports.Find(id);
            if (comentReports == null)
            {
                return HttpNotFound();
            }
            return View(comentReports);
        }

        // GET: ComentReports/Create
        public ActionResult Create()
        {
            ViewBag.ComentariosFK = new SelectList(db.Comentarios, "comentID", "conteudo");
            ViewBag.UtilizadorFK = new SelectList(db.Users, "Id", "Nickname");
            return View();
        }

        // POST: ComentReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ComRepId,razRep,ComentariosFK,UtilizadorFK")] ComentReports comentReports)
        {
            if (ModelState.IsValid)
            {
                db.ComentReports.Add(comentReports);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ComentariosFK = new SelectList(db.Comentarios, "comentID", "conteudo", comentReports.ComentariosFK);
            ViewBag.UtilizadorFK = new SelectList(db.Users, "Id", "Nickname", comentReports.UtilizadorFK);
            return View(comentReports);
        }

        // GET: ComentReports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComentReports comentReports = db.ComentReports.Find(id);
            if (comentReports == null)
            {
                return HttpNotFound();
            }
            ViewBag.ComentariosFK = new SelectList(db.Comentarios, "comentID", "conteudo", comentReports.ComentariosFK);
            ViewBag.UtilizadorFK = new SelectList(db.Users, "Id", "Nickname", comentReports.UtilizadorFK);
            return View(comentReports);
        }

        // POST: ComentReports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ComRepId,razRep,ComentariosFK,UtilizadorFK")] ComentReports comentReports)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comentReports).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ComentariosFK = new SelectList(db.Comentarios, "comentID", "conteudo", comentReports.ComentariosFK);
            ViewBag.UtilizadorFK = new SelectList(db.Users, "Id", "Nickname", comentReports.UtilizadorFK);
            return View(comentReports);
        }

        // GET: ComentReports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComentReports comentReports = db.ComentReports.Find(id);
            if (comentReports == null)
            {
                return HttpNotFound();
            }
            return View(comentReports);
        }

        // POST: ComentReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ComentReports comentReports = db.ComentReports.Find(id);
            db.ComentReports.Remove(comentReports);
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
