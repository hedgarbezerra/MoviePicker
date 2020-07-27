using OurMovies.MoviePicker.Domain.Models;
using OurMovies.MoviePicker.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OurMovies.MoviePicker.MVC.Controllers
{
    [RoutePrefix("api")]
    public class TesteController : ApiController
    {
        [HttpPost]
        [Route("CadastroCategoria")]
        public IHttpActionResult CadastroCategoria([FromBody] Categoria categoria)
        {
            CategoriasRepository repo = new CategoriasRepository();
            var objCtx = repo.Inserir(categoria);
            repo.Savechanges();

            return Ok(new
            {
                data = objCtx,
                message = "Categoria cadastrado com sucesso."
            });
        }

        [HttpPost]
        [Route("CadastroFilme")]
        public IHttpActionResult CadastroFilme([FromBody] Filme filme)
        {
            try
            {
                CategoriasRepository repo2 = new CategoriasRepository();
                FilmesRepository repo = new FilmesRepository();
                var categoria = repo2.Listar().AsEnumerable().FirstOrDefault();
                filme.Categorias.Add(categoria);
                var objCtx = repo.Inserir(filme);
                repo.Savechanges();
                return Ok(new
                {
                    data = objCtx,
                    message = "Filmes cadastrado com sucesso."
                });

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        [HttpPost]
        [Route("CadastrarSenha")]
        public IHttpActionResult CadastrarSenha([FromBody] SenhaAcesso senha)
        {
            try
            {
                SenhasAcessoRepository repo = new SenhasAcessoRepository();
                var objCtx = repo.Inserir(senha);
                repo.Savechanges();
                return Ok(new
                {
                    data = objCtx,
                    message = "senha cadastrado com sucesso."
                });

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpPost]
        [Route("Login")]
        public IHttpActionResult Login([FromBody] SenhaAcesso senha)
        {
            try
            {
                SenhasAcessoRepository repo = new SenhasAcessoRepository();
                SenhaAcesso senhaAutenticada;

                var objCtx = repo.Login(senha, out senhaAutenticada);

                return Ok(new
                {
                    data = objCtx,
                    message = "senha cadastrado com sucesso."
                });

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }



    }
}
