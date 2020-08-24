using OurMovies.MoviePicker.Domain.Models;
using OurMovies.MoviePicker.MVC.Models;
using OurMovies.MoviePicker.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace OurMovies.MoviePicker.MVC.Controllers
{
    [RoutePrefix("api/Categorias")]
    [Authorize]
    public class CategoriasApiController : ApiController
    {
        [HttpPost]
        [Route("Incluir")]
        public IHttpActionResult CadastroCategoria([FromBody] Categoria categoria)
        {
            try
            {
                CategoriasService service = new CategoriasService();
                var objCtx = service.Cadastrar(categoria);

                var response = new 
                {
                    message = "Categoria incluída com sucesso.",
                    data = objCtx,
                    success = true
                };

                if (objCtx != null)
                    return Created(HttpContext.Current.Request.RawUrl, response);
                else
                    return BadRequest("Houve um erro ao cadastrar a nova categoria.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("IncluirDiversas")]
        public IHttpActionResult CadastroCategoriaMultiplas([FromBody] List<Categoria> categorias)
        {
            try
            {
                CategoriasService service = new CategoriasService();
                categorias.ForEach(categoria => service.Cadastrar(categoria));

                var response = new
                {
                    message = "Categorias incluídsa com sucesso.",
                    data = new List<Categoria>(),
                    success = true
                };

                return Created(HttpContext.Current.Request.RawUrl, response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("Remover")]
        public IHttpActionResult ExcluirCategoria(Categoria categoria)
        {
            try
            {
                CategoriasService service = new CategoriasService();
                service.Remover(categoria);

                var response = new DefaultResponse<Categoria>
                {
                    message = "Categoria excluída com sucesso.",
                    data = new List<Categoria>(),
                    success = true
                };
                return Content(HttpStatusCode.Accepted, response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Listar")]
        public IHttpActionResult Listar(string nomeCategoria = "")
        {
            try
            {
                CategoriasService service = new CategoriasService();
                var categorias = service.Listar(nomeCategoria).OrderBy(x => x.Nome).ToList();

                var response = new DefaultResponse<Categoria>
                {
                    message = categorias.Count > 0 ? "Encontramos as seguintes categorias." : "Oops, parece que tem nada por aqui.",
                    data = categorias.Count > 0 ? categorias : new List<Categoria>(),
                    success = true
                };

                var json = Json(response, new Newtonsoft.Json.JsonSerializerSettings 
                { 
                                   
                });

                if (categorias.Count > 0)
                    return Ok(response);
                else
                    return Content(HttpStatusCode.NoContent, response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
