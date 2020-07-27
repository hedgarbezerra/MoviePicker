using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OurMovies.MoviePicker.MVC.Controllers
{
    public class BaseController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}