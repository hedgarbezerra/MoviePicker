using OurMovies.MoviePicker.Domain.DTO;
using OurMovies.MoviePicker.Domain.Models;
using OurMovies.MoviePicker.MVC.Models;
using OurMovies.MoviePicker.Services.Notification;
using OurMovies.MoviePicker.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Security;

namespace OurMovies.MoviePicker.MVC.Controllers
{
    [RoutePrefix("api/Auth")]
    public class AuthController : ApiController
    {
        [HttpPost]
        [Route("Cadastrar")]
        public IHttpActionResult Cadastrar([FromBody] DTOUsuario usuario)
        {
            try
            {
                AutenticacaoService authService = new AutenticacaoService();

                var usuarioCadastrado = authService.Cadastrar(usuario);

                if (usuarioCadastrado != null)
                {
                    var response = new DefaultResponse<SenhaAcesso>
                    {
                        message = "Cadastrado efetuado com sucesso.",
                        data = new List<SenhaAcesso>(),
                        success = true
                    };

                    response.data.Add(usuarioCadastrado);

                    return Created(HttpContext.Current.Request.RawUrl, response);
                }
                else
                    return BadRequest("Houve um erro ao cadastrar.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Login")]
        public IHttpActionResult Login([FromBody] DTOUsuario usuario)
        {
            try
            {
                AutenticacaoService authService = new AutenticacaoService();

                SenhaAcesso usuarioLogado;

                var response = new DefaultResponse<SenhaAcesso>
                {
                    data = new List<SenhaAcesso>()
                };

                if (authService.Autenticar(usuario, out usuarioLogado))
                {
                    FormsAuthentication.SetAuthCookie(usuarioLogado.Usuario, true);
                    response.success = true;
                    response.message = "Conectado(a) com sucesso.";
                    return Ok(response);
                }
                else
                {
                    response.success = false;
                    response.message = "Usuário ou senha incorretos.";

                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("RecuperarSenha")]
        public IHttpActionResult RecuperarSenha([FromBody] DTOContato contato)
        {
            try
            {
                SenhaRecuperacaoService recuperacaoService = new SenhaRecuperacaoService();
                EmailNotification emailNotifier = new EmailNotification(MimeKit.Text.TextFormat.Html);

                recuperacaoService.RecuperSenha(contato, emailNotifier, Domain.Enum.TipoContato.EMAIL);

                return Ok(new {
                        message = "Senha resetada com sucesso, verifique sua caixa de e-mail.",
                        data = new List<string>(),
                        success = true
                    });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    message = $"Oops, não foi possível resetar sua senha pelo seguinte motivo: {ex.Message}",
                    data = new List<string>(),
                    success = false
                });
            }
        }

        [HttpPost]
        [Route("AtualizarSenha")]
        public IHttpActionResult AtualizarSenha([FromBody] DTOUsuario usuario)
        {
            try
            {
                AutenticacaoService autenticacaoService = new AutenticacaoService();

                string usuarioAtual = HttpContext.Current.User.Identity.Name;
                usuario.Usuario = usuarioAtual;

                autenticacaoService.AtualizarSenha(usuario);

                return Ok(new
                {
                    message = "Senha alterada com sucesso.",
                    data = new List<string>(),
                    success = true
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    message = $"Oops, não foi possível atualizar sua senha pelo seguinte motivo: {ex.Message}",
                    data = new List<string>(),
                    success = false
                });
            }
        }
    }
}
