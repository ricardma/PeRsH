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
    public class DiscussoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Discussoes
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var discussoes = db.Discussoes.Include(d => d.User);
            return View(discussoes.ToList());
        }

        // GET: Discussoes/Details/5
        [Authorize(Roles = "Admin")]
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
        /// <summary>
        /// cria uma DISCUSSAO com o título desejado pelo utilizador e o seu conteúdo, nos TEMAS selecionados na checkbox da view
        /// </summary>
        /// <param name="discussoes"></param>
        /// <param name="id"></param>
        /// <param name="checkbox"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idDiscussao,titulo,conteudo")] Discussoes discussoes, int? id,string[] checkbox)
        {
            int idT = 0;
            //caso haja alguma TEMA escolhido
            try
            {
                //para cada TEMA escolhido na checkbox
                foreach (var item in checkbox)
                {
                    idT = Convert.ToInt32(item);
                    //procura o id do TEMA na tabela respetiva
                    Temas tema = db.Temas.Select(x => x).Where(x => x.idTema == idT).FirstOrDefault();
                    //adiciona ao relacionamento de M-M
                    discussoes.TemasDiscussoes.Add(tema);
                }
                discussoes.avaliacao = 0;
                discussoes.dataPublicacao = DateTime.Now;
                discussoes.dislikes = 0;
                discussoes.likes = 0;
                discussoes.UtilizadorFK = User.Identity.GetUserId();
                if (ModelState.IsValid)
                {
                    db.Discussoes.Add(discussoes);
                    db.SaveChanges();
                    return RedirectToAction("PergDisc", "PergDisc", new { id = id });
                }
            }
            //senão houver TEMA escolhido
            catch (Exception)
            {
                IEnumerable<Temas> temas = db.Temas.ToList();
                ViewBag.temas = temas;
                ModelState.AddModelError("", string.Format("Erro! Introduza pelo menos um Tema para a Discussão."));
                return View(discussoes);
            }
            return View(discussoes);
        }

        // GET: Discussoes/Edit/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        /*public ActionResult Delete(int? id)
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
        }*/

        // POST: Discussoes/Delete/5
        //[HttpPost, ActionName("Delete")]
        /// <summary>
        /// apaga uma DISCUSSAO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles="Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Discussoes discussoes = db.Discussoes.Find(id);
            db.Discussoes.Remove(discussoes);
            db.SaveChanges();
            return RedirectToAction("Index","Reports");
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
