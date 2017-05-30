using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrabFinal___PeRsH.Models;

namespace TrabFinal___PeRsH.Controllers
{
    public class PergDiscController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PergDisc
        public ActionResult Index(int? id)
        {
            //Caso o 'id' do TEMA for nulo
            if (id == null)
            {
                //Redirecionará o utilizador para a view INDEX
                return RedirectToAction("Index","Temas");
            }
            //Procura um tema com o 'id' fornecido pela view
            Temas temas = db.Temas.Find(id);
            //verifica se o tema foi encontrado
            if (temas == null)
            {
                //caso o tema não seja encontrado, redireciona para a view INDEX
                return RedirectToAction("Index","Temas");
            }
            HomeViewModel hvm = new HomeViewModel();
            hvm.temas = db.Temas.ToList();
            hvm.discussoes = (from disc in db.Discussoes where disc.TemasDiscussoes.Any(t => t.idTema == temas.idTema) select disc).ToList();
            hvm.comentarios = db.Comentarios.Select(x => x);

            var temaEscolhido = db.Temas.Select(x => x).Where(x => x.idTema == id).FirstOrDefault().Etiqueta;
            ViewBag.temaEsc = id;
            ViewBag.temaEscolhido = temaEscolhido;

            return View(hvm);
        }

        /*public ActionResult Index(string a)
        {
            return "";
        }*/
    }
}