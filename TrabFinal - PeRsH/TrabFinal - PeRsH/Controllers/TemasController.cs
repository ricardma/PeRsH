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
    public class TemasController : Controller
    {
        //Criar uma instância que representa a Base de Dados
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Temas
        public ActionResult Index()
        {
            //Retorna a VIEW, com a lista de Temas
            return View(db.Temas.ToList());
        }

        // GET: Temas/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            //Caso o 'id' do TEMA for nulo
            if (id == null)
            {
                //Redirecionará o utilizador para a view INDEX
                return RedirectToAction("Index");
            }
            //Procura um tema com o 'id' fornecido pela view
            Temas temas = db.Temas.Find(id);
            //verifica se o tema foi encontrado
            if (temas == null)
            {
                //caso o tema não seja encontrado, redireciona para a view INDEX
                return RedirectToAction("Index");
            }

            //retorna a view INDEX com os dados do tema
            return View(temas);
        }

        // GET: Temas/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Temas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idTema,Etiqueta")] Temas temas)
        {
            //verifica se os dados introduzidos estão consistentes com o Model
            if (ModelState.IsValid)
            {
                //se os dados forem válidos, adiciona o TEMA
                db.Temas.Add(temas);
                //guarda as alterações
                db.SaveChanges();
                //redireciona a view INDEX
                return RedirectToAction("Index");
            }
            //retorna a view com um tema
            return View(temas);
        }

        // GET: Temas/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            //verifica se o 'id' do TEMA é nulo
            if (id == null)
            {
                //se for, redireciona para a view INDEX
                return RedirectToAction("Index");
            }
            //procura o tema com o 'id' fornecido pela view
            Temas temas = db.Temas.Find(id);
            //verifica se o TEMA foi encontrado
            if (temas == null)
            {
                //caso o 'id' seja nulo redireciona para a view INDEX
                return RedirectToAction("Index");
            }
            //retorna a view com o TEMA
            return View(temas);
        }

        // POST: Temas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "idTema,Etiqueta")] Temas temas)
        {
            //confronta se os dados introduzidos pelo Model são consistentes
            if (ModelState.IsValid)
            {
                //modifica os dados do TEMA
                db.Entry(temas).State = EntityState.Modified;
                //guarda as alterações
                db.SaveChanges();
                //redireciona para a view INDEX
                return RedirectToAction("Index");
            }
            //retorna a view com um tema
            return View(temas);
        }

        // GET: Temas/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            //caso o 'id' do TEMA seja nulo
            if (id == null)
            {
                //redireciona para a view INDEX
                return RedirectToAction("Index");
            }
            //procura o tema com o 'id' fornecido pela view
            Temas temas = db.Temas.Find(id);
            //verifica se o tema foi encontrado
            if (temas == null)
            {
                //se não for, redireciona para a view INDEX
                return RedirectToAction("Index");
            }
            //retorna a view com os dados do TEMA
            return View(temas);
        }

        // POST: Temas/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //procura o TEMA na base de dados
            Temas temas = db.Temas.Find(id);
            try
            {
                //marca um tema para eliminação
                db.Temas.Remove(temas);
                //guarda as alterações
                db.SaveChanges();
                //redireciona para a view INDEX
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                //cria uma mensagem de erro para o utilizador
                ModelState.AddModelError("", string.Format("O tema {0} com o id {1} não pode ser apagado, ocorreu um erro!", temas.Etiqueta, id));
                //invoca a view com os dados do TEMA
                return View(temas);
            }
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
