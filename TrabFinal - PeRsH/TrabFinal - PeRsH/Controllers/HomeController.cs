using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrabFinal___PeRsH.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Descrição da aplicação.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Página de Contactos.";

            return View();
        }
    }
}