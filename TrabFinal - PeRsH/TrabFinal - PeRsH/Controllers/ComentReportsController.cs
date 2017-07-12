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
    public class ComentReportsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ComentReports
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var comentReports = db.ComentReports.Include(c => c.Comentarios).Include(c => c.User);
            ViewBag.comentarios = db.Comentarios.Select(x => x);
            return View(comentReports.ToList());
        }

        // GET: ComentReports/Details/5
        [Authorize(Roles = "Admin")]
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
        /*[Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.ComentariosFK = new SelectList(db.Comentarios, "comentID", "conteudo");
            ViewBag.UtilizadorFK = new SelectList(db.Users, "Id", "Nickname");
            return View();
        }*/

        // POST: ComentReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// cria uma nova denúncia de um COMENTARIO através do id da DISCUSSAO e do COMENTARIO
        /// </summary>
        /// <param name="textAreaReportComent"></param>
        /// <param name="idDisc"></param>
        /// <param name="idComent"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int? idDisc, int idComent)
            {
            int idD = Convert.ToInt32(idDisc);
            int idC = Convert.ToInt32(idComent);
                ComentReports rep = new ComentReports();
                rep.razRep = "Sem Motivo";
                rep.ComentariosFK = idC;
                rep.UtilizadorFK = User.Identity.GetUserId();
                db.ComentReports.Add(rep);
                db.SaveChanges();
                return RedirectToAction("PergDisc", "PergDisc", new { id = idD });
        }

        // GET: ComentReports/Edit/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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

        /// <summary>
        /// modifica o estado de uma denúncia a um COMENTARIO de não visto para visto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult VisualizarComentDenunc(int id)
        {
            ViewBag.comentarios = db.Comentarios.Select(x => x);
            //procura o COMENTARIO com o id passado por parâmetro
            ComentReports rep = db.ComentReports.Select(x => x).Where(x => x.ComentariosFK == id).FirstOrDefault();
            //modifica o estado da variável "visto" para true
            rep.visto = true;
            db.SaveChanges();
            var reports = db.ComentReports.Include(r => r.Comentarios).Include(r => r.User);
            return View("Index",reports);
        }

        // GET: ComentReports/Delete/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.comentarios = db.Comentarios.Select(x => x);
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
