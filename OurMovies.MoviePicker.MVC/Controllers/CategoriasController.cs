using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OurMovies.MoviePicker.MVC.Controllers
{
    [Authorize]
    public class CategoriasController : Controller
    {
        public ActionResult Cadastrar()
        {
            return View();
        }
    }
}