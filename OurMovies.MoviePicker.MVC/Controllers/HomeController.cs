using OurMovies.MoviePicker.Domain.DTO;
using OurMovies.MoviePicker.Domain.Models;
using OurMovies.MoviePicker.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OurMovies.MoviePicker.MVC.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult CadastrarFilme()
        {
            return View();
        }

        [Authorize]
        public ActionResult CadastrarCategoria()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(DTOUsuario usuario)
        {
            AutenticacaoService authService = new AutenticacaoService();

            SenhaAcesso usuarioLogado;

            if(authService.Autenticar(usuario, out usuarioLogado))
            {
                FormsAuthentication.SetAuthCookie(usuarioLogado.Usuario, true);

                return View("Index");
            }
            else
            {
                return View();
            }
        }

    }
}