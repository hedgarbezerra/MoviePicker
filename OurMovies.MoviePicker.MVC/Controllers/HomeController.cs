using OurMovies.MoviePicker.Domain.DTO;
using OurMovies.MoviePicker.Domain.Models;
using OurMovies.MoviePicker.Services.Notification;
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
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }

        public ActionResult RecuperarSenha(string token)
        {
            return View();
        }

        [HttpPost]
        public JsonResult RecuperarSenha(DTOContato contato)
        {
            try
            {
                SenhaRecuperacaoService service = new SenhaRecuperacaoService();

                EmailNotification emailNotifier = new EmailNotification(MimeKit.Text.TextFormat.Html);

                string url = Url.Action("RecuperarSenha", "Home", null, protocol: Request.Url.Scheme);

                service.RecuperarSenhaToken(contato, emailNotifier, url);

                return Json(new
                {
                    success = true,
                    message = $"E-mail de reset de senha enviado para {contato.Contato}",
                    data = new List<string>()
                });

            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message,
                    data = new List<string>()
                });
            }

        }

        [Authorize]
        public ActionResult AlterarSenha()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                return RedirectToAction("Index");

            return View();
        }

        [AllowAnonymous]
        public ActionResult Cadastrar()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                return RedirectToAction("Index");

            AutenticacaoService service = new AutenticacaoService();

            var qtdUsuarios = service.Listar().Count;

            if (qtdUsuarios >= 2)
                return RedirectToActionPermanent("Login");

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(DTOUsuario usuario)
        {
            AutenticacaoService authService = new AutenticacaoService();

            SenhaAcesso usuarioLogado;

            if (authService.Autenticar(usuario, out usuarioLogado))
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