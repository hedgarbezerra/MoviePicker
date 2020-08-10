using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OurMovies.MoviePicker.MVC.Controllers
{
    public class FilmesController : Controller
    {
        // GET: Filmes
        public ActionResult Cadastrar()
        {
            return View();
        }
        public ActionResult Aleatorio()
        {
            return View();
        }
    }
}