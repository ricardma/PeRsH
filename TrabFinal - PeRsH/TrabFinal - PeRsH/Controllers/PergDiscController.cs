using Microsoft.AspNet.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using TrabFinal___PeRsH.Models;

namespace TrabFinal___PeRsH.Controllers
{
    public class PergDiscController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Index
        /// <summary>
        /// seleciona os dados DISCUSSOES, COMENTARIOS, LIKES, DISLIKES, AVALIACAO, REPORTS E COMENTREPORTS para a VIEW Index 
        /// só são enviadas as DISCUSSOES que tenham o id do TEMA ao que é passado por parâmetro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Index(int? id)
        {
            //Caso o 'id' do TEMA for nulo
            if (id == null)
            {
                //Redirecionará o utilizador para a view INDEX
                return RedirectToAction("Index", "Temas");
            }
            //Procura um tema com o 'id' fornecido pela view
            Temas temas = db.Temas.Find(id);
            //verifica se o tema foi encontrado
            if (temas == null)
            {
                //caso o tema não seja encontrado, redireciona para a view INDEX
                return RedirectToAction("Index", "Temas");
            }

            //ViewModel a ser utilizada na view com os TEMAS, DISCUSSOES, COMENTARIOS, LIKES, DISLIKES, AVALAICAO, REPORTS E COMENTREPORTS
            HomeViewModel hvm = new HomeViewModel();
            hvm.temas = db.Temas.ToList();
            hvm.discussoes = (from disc in db.Discussoes where disc.TemasDiscussoes.Any(t => t.idTema == temas.idTema) select disc).ToList();
            hvm.comentarios = db.Comentarios.Select(x => x);
            hvm.likes = db.Likes.Select(x => x);
            hvm.dislikes = db.Dislikes.Select(x => x);
            hvm.avaliacao = db.Avaliacao.Select(x => x).ToList();
            hvm.reports = db.Reports.Select(x => x).ToList();
            hvm.comentReports = db.ComentReports.Select(x => x).ToList();

            var temaEscolhido = db.Temas.Select(x => x).Where(x => x.idTema == id).FirstOrDefault().Etiqueta;
            ViewBag.numDisc = db.Discussoes.Select(x => x).Count();

            ViewBag.temaEsc = id;
            ViewBag.temaEscolhido = temaEscolhido;
            ViewBag.temas = hvm.temas.Select(x => x.Etiqueta);

            return View(hvm);
        }

        /// <summary>
        /// pesquisa quais as discussões 
        /// </summary>
        /// <param name="btnPesq"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Pesquisa(string btnPesq)
        {
            //Retirado do site https://stackoverflow.com *************************************************
            //permite converter uma string em palavras separadas
            MatchCollection palavras = Regex.Matches(btnPesq.ToString(), @"\b[\w']*\b");
            //seleciona o valor de cada palavra
            var palavra = from p in palavras.Cast<Match>() where !string.IsNullOrEmpty(p.Value) select p.Value;
            //adiciona a um array
            string[] list = palavra.ToArray();
            //********************************************************************************************

            List<Discussoes> lista = new List<Discussoes>();

            foreach (var item in list)
            {
                //para cada palavra, seleciona a discussão e adiciona-a a uma lista de DISCUSSOES
                var s = (from discs in db.Discussoes where discs.conteudo.Contains(item) || discs.titulo.Contains(item) select discs).Distinct().ToList();
                lista.AddRange(s);
            }

            HomeViewModel hvm = new HomeViewModel();
            hvm.temas = db.Temas.Select(x => x).ToList();
            hvm.discussoes = lista.Select(x => x).Distinct().ToList();
            hvm.comentarios = db.Comentarios.Select(x => x).ToList();
            hvm.likes = db.Likes.Select(x => x).ToList();
            hvm.dislikes = db.Dislikes.Select(x => x).ToList();
            hvm.avaliacao = db.Avaliacao.Select(x => x).ToList();

            ViewBag.listPesq = list;

            return View("PesqPerg",hvm);
        }

        // GET: PergDisc
        /// <summary>
        /// seleciona os dados para a VIEW PergDisc
        /// os dados são a DISCUSSOES que tem o id igual ao que é passado por parâmetro
        /// os COMENTARIOS, OS LIKES, DISLIKES, AVALAICAO, REPORTS e COMENTREPORTS
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult PergDisc(int? id)
        {
            //Caso o 'id' do TEMA for nulo
            if (id == null)
            {
                //Redirecionará o utilizador para a view INDEX
                return RedirectToAction("Index","Temas");
            }
            //Procura um tema com o 'id' fornecido pela view
            Discussoes discussoes = db.Discussoes.Find(id);
            //verifica se o tema foi encontrado
            if (discussoes == null)
            {
                //caso o tema não seja encontrado, redireciona para a view INDEX
                return RedirectToAction("Index","Temas");
            }
            
            HomeViewModel hvm = new HomeViewModel();
            hvm.temas = db.Temas.ToList();
            hvm.discussoes = (from disc in db.Discussoes where disc.idDiscussao == id select disc).ToList();
            hvm.comentarios = db.Comentarios.Select(x => x).Where(x=>x.DiscussaoFK==id).ToList();
            hvm.likes = db.Likes.Select(x => x).Where(x => x.DiscussaoFK == id).ToList();
            hvm.dislikes = db.Dislikes.Select(x => x).Where(x => x.DiscussaoFK == id).ToList();
            hvm.avaliacao = db.Avaliacao.Select(x => x).Where(x => x.DiscussaoFK == id).ToList();
            hvm.reports = db.Reports.Select(x => x).Where(x => x.DiscussaoFK == id).ToList();
            hvm.comentReports = db.ComentReports.Select(x => x).ToList();

            var temaEscolhido = db.Temas.Select(x => new { x.idTema, x.TemasDiscussoes, x.Etiqueta }).Where(x => x.TemasDiscussoes.Any(y => y.idDiscussao == id)).FirstOrDefault();
            ViewBag.temaEsc = temaEscolhido.idTema;
            ViewBag.temaEscolhido = temaEscolhido.Etiqueta;
            ViewBag.likes = db.Likes.Select(x => x).Where(x=>x.DiscussaoFK==id).Count();
            ViewBag.dislikes = db.Dislikes.Select(x => x).Where(x=>x.DiscussaoFK==id).Count();
            ViewBag.numDisc = db.Discussoes.Select(x => x).Count();
            ViewBag.temas = db.Temas.Select(x => x.Etiqueta).ToList();

            return View(hvm);
        }

        /// <summary>
        /// vota LIKE ou DISLIKE na DISCUSSAO passada por parâmetro
        /// </summary>
        /// <param name="id"></param>
        /// <param name="btnAction"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult Vote(int id,string btnAction)
        {
            string option = btnAction.ToString();
            var userId = User.Identity.GetUserId();
            //procura na base de dados a DISCUSSAO com o id passado por parâmetro
            Discussoes discs = db.Discussoes.Find(id);
            //procura na base de dados se a DISCUSSAO TEM LIKES OU DISLIKES
            Likes li = db.Likes.Select(x => x).Where(x => x.DiscussaoFK == id).Where(x=>x.ComentarioFK==null).Where(x=>x.UtilizadorFK==userId).FirstOrDefault();
            Dislikes dis = db.Dislikes.Select(x => x).Where(x => x.DiscussaoFK == id).Where(x=>x.ComentarioFK==null).Where(x=>x.UtilizadorFK==userId).FirstOrDefault();

            switch (option)
            {
                case "likesDisc":

                    if(discs == null)
                    {
                        return HttpNotFound();
                    }

                    if (li == null)
                    {
                        discs.likes = discs.likes + 1;
                        var newId = db.Likes.Select(x => x.likeID).Count() + 1;
                        //cria um novo LIKE
                        var newLike = new Likes { likeID = newId, dataLike = DateTime.Now, DiscussaoFK = id, UtilizadorFK = userId };
                        //adiciona na BD
                        db.Likes.Add(newLike);
                        Session["stateVoteDisc"] = true;
                    }
                    db.SaveChanges();
                    break;
                case "dislikesDisc":

                    if (discs == null)
                    {
                        return HttpNotFound();
                    }

                    if (dis == null)
                    {
                        discs.dislikes = discs.dislikes + 1;
                        var newId = db.Dislikes.Select(x => x.dislikeID).Count() + 1;
                        //cria um novo DISLIKE
                        var newDislike = new Dislikes { dislikeID = newId, dataLike = DateTime.Now, DiscussaoFK = id, UtilizadorFK = userId };
                        //adiciona na BD
                        db.Dislikes.Add(newDislike);
                        Session["stateVoteDiscDislike"] = true;
                    }
                    db.SaveChanges();
                    break;
            }

            return RedirectToAction("PergDisc","PergDisc",new { id = id});
        }

        /// <summary>
        /// retira a votação da DISCUSSAO caso exista
        /// </summary>
        /// <param name="id"></param>
        /// <param name="btnAction"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult RetVote(int id, string btnAction)
        {
            string option = btnAction.ToString();
            var userId = User.Identity.GetUserId();
            Discussoes discs = db.Discussoes.Find(id);
            Likes li = db.Likes.Select(x => x).Where(x => x.DiscussaoFK == id).FirstOrDefault();
            Dislikes dis = db.Dislikes.Select(x => x).Where(x => x.DiscussaoFK == id).FirstOrDefault();

            switch (option)
            {
                case "likesDisc":
                    discs.likes = discs.likes - 1;
                    db.Likes.Remove(li);
                    Session["stateVoteDisc"] = false;
                    db.SaveChanges();
                    break;
                case "dislikesDisc":
                    discs.dislikes = discs.dislikes - 1;
                    db.Dislikes.Remove(dis);
                    Session["stateVoteDiscDislike"] = false;
                    db.SaveChanges();
                    break;
            }


            return RedirectToAction("PergDisc", "PergDisc", new { id = id });
        }

        /// <summary>
        /// avalia a DISCUSSAO com a pontuação de 1 a 5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="btnAvalDisc"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult avaliaDisc(int id, string btnAvalDisc)
        {
            string option = btnAvalDisc.ToString();
            int valueOption = Convert.ToInt16(option.Substring(8, 1));
            //recolhe o id do utilizador autenticado
            var userId = User.Identity.GetUserId();
            //procura a DISCUSSAO com o id igual ao passado no parâmetro
            Discussoes discs = db.Discussoes.Find(id);
            //procura na BD se este utilizador já tem avaliacao nas DISCUSSOES
            Avaliacao av = db.Avaliacao.Select(x => x).Where(x => x.DiscussaoFK == id).Where(x=>x.UtilizadorFK==userId).FirstOrDefault();

            if (discs == null)
            {
                return HttpNotFound();
            }

            if (av == null)
            {
                var newId = db.Likes.Select(x => x.likeID).ToList().Count() + 1;
                //cria uma nova AVALIACAO
                var newAval = new Avaliacao { avaliacaoID = newId, dataLike = DateTime.Now,avaliacao = valueOption,  DiscussaoFK = id, UtilizadorFK = userId };
                db.Avaliacao.Add(newAval);
            }
            //grava as notificacoes
            db.SaveChanges();

            //faz a média da avaliação da DISCUSSAO
            discs.avaliacao = (from aval in db.Avaliacao where (aval.DiscussaoFK == id && aval.ComentarioFK==null) select aval.avaliacao).ToList().Average();
            db.SaveChanges();

            return RedirectToAction("PergDisc", "PergDisc", new { id = id });
        }

        /// <summary>
        /// avalia o COMENTARIO
        /// </summary>
        /// <param name="idComent"></param>
        /// <param name="idDisc"></param>
        /// <param name="btnAvalComent"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult avaliaComent(int idComent,int idDisc,string btnAvalComent)
        {
            string option = btnAvalComent.ToString();
            int valueOption = Convert.ToInt16(option.Substring(10, 1));
            int notaAval = valueOption;

            //recolhe o id do utilizador autenticado
            var userId = User.Identity.GetUserId();
            //procura a DISCUSSAO com o id igual ao passado por parâmetro
            Discussoes discs = db.Discussoes.Find(idDisc);
            //procura o COMENTARIO com o id igual ao passado por parâmetro
            Comentarios coment = db.Comentarios.Find(idComent);
            //procura na BD se este utilizador já tem avaliacao nos COMENTARIO
            Avaliacao av = db.Avaliacao.Select(x => x).Where(x => x.ComentarioFK == idComent).Where(x=>x.UtilizadorFK==userId).FirstOrDefault();

            if (coment == null)
            {
                return HttpNotFound();
            }

            if (av == null)
            {
                var newId = db.Likes.Select(x => x.likeID).Count() + 1;
                //cria uma nova avaliacao
                var newAval = new Avaliacao { avaliacaoID = newId, dataLike = DateTime.Now, avaliacao = notaAval, DiscussaoFK = idDisc  ,ComentarioFK = idComent, UtilizadorFK = userId };
                db.Avaliacao.Add(newAval);
            }
            //grava as modificações
            db.SaveChanges();

            //faz a média da avaliação do COMENTARIO
            coment.avaliacao = (from aval in db.Avaliacao where aval.ComentarioFK==idComent select aval.avaliacao).ToList().Average();
            db.SaveChanges();

            return RedirectToAction("PergDisc", "PergDisc", new { id = idDisc });
        }

        /// <summary>
        /// vota LIKE ou DISLIKE num COMENTARIO
        /// </summary>
        /// <param name="idDisc"></param>
        /// <param name="idComent"></param>
        /// <param name="btnAction"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult VoteComent(int idDisc,int idComent, string btnAction)
        {
            string option = btnAction.ToString();
            var userId = User.Identity.GetUserId();
            Discussoes discs = db.Discussoes.Find(idDisc);
            //Verfica se o COMENTARIO com o ID passado por parâmetro tem algum like ou dislike
            Likes li = db.Likes.Select(x => x).Where(x => x.DiscussaoFK == idDisc).Where(x=>x.ComentarioFK==idComent).FirstOrDefault();
            Dislikes dis = db.Dislikes.Select(x => x).Where(x => x.DiscussaoFK == idDisc).Where(x=>x.ComentarioFK==idComent).FirstOrDefault();

            switch (option)
            {
                case "likesComent":

                    if (discs == null)
                    {
                        return HttpNotFound();
                    }

                    if (li == null)
                    {
                        discs.likes = discs.likes + 1;
                        var newId = db.Likes.Select(x => x.likeID).Count() + 1;
                        var newLike = new Likes { likeID = newId, dataLike = DateTime.Now, DiscussaoFK = idDisc, ComentarioFK=idComent, UtilizadorFK = userId };
                        db.Likes.Add(newLike);
                    }
                    db.SaveChanges();
                    break;
                case "dislikesComent":

                    if (discs == null)
                    {
                        return HttpNotFound();
                    }

                    if (dis == null)
                    {
                        discs.dislikes = discs.dislikes + 1;
                        var newId = db.Dislikes.Select(x => x.dislikeID).Count() + 1;
                        var newDislike = new Dislikes { dislikeID = newId, dataLike = DateTime.Now, DiscussaoFK = idDisc, ComentarioFK=idComent, UtilizadorFK = userId };
                        db.Dislikes.Add(newDislike);
                    }
                    db.SaveChanges();
                    break;
            }

            return RedirectToAction("PergDisc", "PergDisc", new { id = idDisc });
        }

        /// <summary>
        /// retira a votação de um COMETARIO
        /// </summary>
        /// <param name="idDisc"></param>
        /// <param name="idComent"></param>
        /// <param name="btnAction"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult RetVoteComent(int idDisc,int idComent, string btnAction)
        {
            string option = btnAction.ToString();
            var userId = User.Identity.GetUserId();
            Discussoes discs = db.Discussoes.Find(idDisc);
            //seleciona os LIKES e DISLIKES do COMENTARIO com o id igual ao id passado por parâmetro
            Likes li = db.Likes.Select(x => x).Where(x => x.DiscussaoFK == idDisc).Where(x=>x.ComentarioFK==idComent).Where(x=>x.UtilizadorFK==userId).FirstOrDefault();
            Dislikes dis = db.Dislikes.Select(x => x).Where(x => x.DiscussaoFK == idDisc).Where(x => x.ComentarioFK == idComent).Where(x => x.UtilizadorFK == userId).FirstOrDefault();

            switch (option)
            {
                case "likesComent":
                    discs.likes = discs.likes - 1;
                    db.Likes.Remove(li);
                    db.SaveChanges();
                    break;
                case "dislikesComent":
                    discs.dislikes = discs.dislikes - 1;
                    db.Dislikes.Remove(dis);
                    db.SaveChanges();
                    break;
            }


            return RedirectToAction("PergDisc", "PergDisc", new { id = idDisc });
        }
    }
}