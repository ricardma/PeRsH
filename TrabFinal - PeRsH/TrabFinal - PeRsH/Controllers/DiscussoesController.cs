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
    public class DiscussoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Discussoes
        public ActionResult Index()
        {
            var discussoes = db.Discussoes.Include(d => d.User);
            return View(discussoes.ToList());
        }

        // GET: Discussoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discussoes discussoes = db.Discussoes.Find(id);
            if (discussoes == null)
            {
                return HttpNotFound();
            }
            return View(discussoes);
        }

        // GET: Discussoes/Create
        public ActionResult Create(int? id)
        {
            ViewBag.UtilizadorFK = new SelectList(db.Users, "Id", "Nickname");
            ViewBag.temaEscolhido = id;
            IEnumerable<Temas> temas = db.Temas.ToList();
            ViewBag.temas = temas;
            return View();
        }

        // POST: Discussoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idDiscussao,titulo,conteudo")] Discussoes discussoes, int? id,string[] checkbox)
        {
            int idT = 0;
            foreach(var item in checkbox)
            {
                idT = Convert.ToInt32(item);
                Temas tema = db.Temas.Select(x => x).Where(x => x.idTema == idT).FirstOrDefault();
                discussoes.TemasDiscussoes.Add(tema);
            }
            discussoes.avaliacao = 0;
            discussoes.dataPublicacao = DateTime.Now;
            discussoes.dislikes = 0;
            discussoes.likes = 0;
            discussoes.report = 0;
            discussoes.UtilizadorFK = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Discussoes.Add(discussoes);
                db.SaveChanges();
                return RedirectToAction("PergDisc","PergDisc", new { id = id});
            }

            ViewBag.UtilizadorFK = new SelectList(db.Users, "Id", "Nickname", discussoes.UtilizadorFK);
            return View(discussoes);
        }

        // GET: Discussoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discussoes discussoes = db.Discussoes.Find(id);
            if (discussoes == null)
            {
                return HttpNotFound();
            }
            ViewBag.UtilizadorFK = new SelectList(db.Users, "Id", "Nickname", discussoes.UtilizadorFK);
            return View(discussoes);
        }

        // POST: Discussoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idDiscussao,dataPublicacao,titulo,conteudo,likes,dislikes,report,UtilizadorFK")] Discussoes discussoes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(discussoes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UtilizadorFK = new SelectList(db.Users, "Id", "Nickname", discussoes.UtilizadorFK);
            return View(discussoes);
        }

        // GET: Discussoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discussoes discussoes = db.Discussoes.Find(id);
            if (discussoes == null)
            {
                return HttpNotFound();
            }
            return View(discussoes);
        }

        // POST: Discussoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Discussoes discussoes = db.Discussoes.Find(id);
            db.Discussoes.Remove(discussoes);
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
