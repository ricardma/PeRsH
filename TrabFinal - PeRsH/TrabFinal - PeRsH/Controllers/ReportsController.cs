using Microsoft.AspNet.Identity;
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
    [Authorize]
    public class ReportsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reports
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var reports = db.Reports.Include(r => r.Discussoes).Include(r => r.User);
            return View(reports.ToList());
        }


        // GET: Reports/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reports reports = db.Reports.Find(id);
            if (reports == null)
            {
                return HttpNotFound();
            }
            return View(reports);
        }

        // GET: Reports/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.DiscussaoFK = new SelectList(db.Discussoes, "idDiscussao", "titulo");
            ViewBag.UtilizadorFK = new SelectList(db.Users, "Id", "Nickname");
            return View();
        }

        // POST: Reports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// cria um report a uma DISCUSSAO
        /// </summary>
        /// <param name="textAreaReportDisc"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string textAreaReportDisc, int? id)
        {
            int idDisc = Convert.ToInt32(id);
            //se a razão do REPORT não for nulo ou vazio
            if (!string.IsNullOrEmpty(textAreaReportDisc))
            {
                Reports rep = new Reports();
                rep.razRep = textAreaReportDisc;
                rep.DiscussaoFK = idDisc;
                rep.UtilizadorFK = User.Identity.GetUserId();
                db.Reports.Add(rep);
                db.SaveChanges();
                return RedirectToAction("PergDisc", "PergDisc", new { id = idDisc });
            }
            else
            {
                Reports rep = new Reports();
                rep.razRep = "Sem Motivo";
                rep.DiscussaoFK = idDisc;
                rep.UtilizadorFK = User.Identity.GetUserId();
                db.Reports.Add(rep);
                db.SaveChanges();
                return RedirectToAction("PergDisc", "PergDisc", new { id = idDisc });
            }   
        }

        // GET: Reports/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reports reports = db.Reports.Find(id);
            if (reports == null)
            {
                return HttpNotFound();
            }
            ViewBag.DiscussaoFK = new SelectList(db.Discussoes, "idDiscussao", "titulo", reports.DiscussaoFK);
            ViewBag.UtilizadorFK = new SelectList(db.Users, "Id", "Nickname", reports.UtilizadorFK);
            return View(reports);
        }

        // POST: Reports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RepId,razRep,DiscussaoFK,UtilizadorFK")] Reports reports)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reports).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DiscussaoFK = new SelectList(db.Discussoes, "idDiscussao", "titulo", reports.DiscussaoFK);
            ViewBag.UtilizadorFK = new SelectList(db.Users, "Id", "Nickname", reports.UtilizadorFK);
            return View(reports);
        }

        // GET: Reports/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reports reports = db.Reports.Find(id);
            if (reports == null)
            {
                return HttpNotFound();
            }
            return View(reports);
        }

        /// <summary>
        /// marca um REPORT como visto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult VisualizarDiscDenunc(int id)
        {
            Reports rep = db.Reports.Select(x=>x).Where(x=>x.DiscussaoFK==id).FirstOrDefault();
            rep.visto = true;
            db.SaveChanges();
            var reports = db.Reports.Include(r => r.Discussoes).Include(r => r.User);
            return View("Index",reports);
        }

        // POST: Reports/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reports reports = db.Reports.Find(id);
            db.Reports.Remove(reports);
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
