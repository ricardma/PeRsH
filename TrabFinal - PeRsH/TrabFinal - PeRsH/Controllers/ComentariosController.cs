﻿using Microsoft.AspNet.Identity;
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
    public class ComentariosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comentarios
        public ActionResult Index()
        {
            var comentarios = db.Comentarios.Include(c => c.Discussoes).Include(c => c.User);
            return View(comentarios.ToList());
        }

        // GET: Comentarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentarios comentarios = db.Comentarios.Find(id);
            if (comentarios == null)
            {
                return HttpNotFound();
            }
            return View(comentarios);
        }

        // GET: Comentarios/Create
        public ActionResult Create()
        {
            ViewBag.DiscussaoFK = new SelectList(db.Discussoes, "idDiscussao", "titulo");
            ViewBag.UtilizadorFK = new SelectList(db.Users, "Id", "Nickname");
            return View();
        }

        // POST: Comentarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int? id,string textArea)
        {
            int idDisc = Convert.ToInt32(id);
            Comentarios coment = new Comentarios();
            if (!string.IsNullOrEmpty(textArea))
            {
                coment.conteudo = textArea;
                coment.dataComentario = DateTime.Now;
                coment.likes = 0;
                coment.dislikes = 0;
                coment.report = 0;
                coment.DiscussaoFK = idDisc;
                coment.UtilizadorFK = User.Identity.GetUserId();
                db.Comentarios.Add(coment);
                db.SaveChanges();
                return RedirectToAction("PergDisc","PergDisc",new { id = id });
            }
            ModelState.AddModelError("", "Erro! O comentário não pode ser vazio!");
            return View("PergDisc","PergDisc", new { id = id });
        }

        // GET: Comentarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentarios comentarios = db.Comentarios.Find(id);
            if (comentarios == null)
            {
                return HttpNotFound();
            }
            ViewBag.DiscussaoFK = new SelectList(db.Discussoes, "idDiscussao", "titulo", comentarios.DiscussaoFK);
            ViewBag.UtilizadorFK = new SelectList(db.Users, "Id", "Nickname", comentarios.UtilizadorFK);
            return View(comentarios);
        }

        // POST: Comentarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "comentID,conteudo,dataComentario,likes,dislikes,report,DiscussaoFK,UtilizadorFK")] Comentarios comentarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comentarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DiscussaoFK = new SelectList(db.Discussoes, "idDiscussao", "titulo", comentarios.DiscussaoFK);
            ViewBag.UtilizadorFK = new SelectList(db.Users, "Id", "Nickname", comentarios.UtilizadorFK);
            return View(comentarios);
        }

        // GET: Comentarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentarios comentarios = db.Comentarios.Find(id);
            if (comentarios == null)
            {
                return HttpNotFound();
            }
            return View(comentarios);
        }

        // POST: Comentarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comentarios comentarios = db.Comentarios.Find(id);
            db.Comentarios.Remove(comentarios);
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
