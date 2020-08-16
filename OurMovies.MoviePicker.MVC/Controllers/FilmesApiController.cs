using Microsoft.Ajax.Utilities;
using OurMovies.MoviePicker.Domain.Models;
using OurMovies.MoviePicker.MVC.Models;
using OurMovies.MoviePicker.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OurMovies.MoviePicker.MVC.Controllers
{
    [RoutePrefix("api/Filmes")]
    public class FilmesApiController : ApiController
    {
        [HttpPost]
        [Route("Incluir")]
        public IHttpActionResult CadastroFilme([FromBody] Filme filme)
        {
            try
            {
                FilmesService service = new FilmesService();
                var objCtx = service.Cadastrar(filme);

                return Ok(new 
                {
                    data = objCtx,
                    message = "Filme incluído com sucesso.",
                    success = true
                });

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("IncluirDiversos")]
        public IHttpActionResult CadastroFilmeMultiplos([FromBody] List<Filme> filmes)
        {
            try
            {
                FilmesService service = new FilmesService();

                filmes.ForEach(x => service.Cadastrar(x));

                return Ok(new
                {
                    data = new List<Filme>(),
                    message = "Filmes incluídos com sucesso.",
                    success = true
                });

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("Remover")]
        public IHttpActionResult RemoverFilme([FromBody] Filme filme)
        {
            try
            {
                FilmesService service = new FilmesService();

                var filmeCtx = service.GetFilme(filme);

                service.RemoverFilme(filmeCtx);

                return Ok(new
                {
                    data = filmeCtx,
                    message = "Filme removido com sucesso.",
                    success = true
                });

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("ListarPorCategoria")]
        public IHttpActionResult ListarPorCategoria([FromBody] List<Categoria> categorias)
        {
            try
            {
                CategoriasService service = new CategoriasService();
                var listaFilmes = service.ListarPorCategoria(categorias, out string categoriasString).OrderBy(x => x.Nome).ThenByDescending(x => x.DtAdicionado).ThenByDescending(x => x.DtAdicionado).ToList();

                var response = new DefaultResponse<Categoria>
                {
                    data = listaFilmes,
                    message = listaFilmes.Count > 0 ? $"Filmes das categorias: {categoriasString} encontrados." : $"Filmes das categorias: {categoriasString} não foram encontrados.",
                    success = true
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("ListarAssistido")]
        public IHttpActionResult ListarAssistido([FromUri] bool assistido = false)
        {
            try
            {
                FilmesService service = new FilmesService();
                var listaFilmes = service.ListarFilmes(assistido).OrderBy(x => x.Nome).ThenByDescending(x => x.DtAdicionado).ToList();
                var assistidoString = assistido ? "assistidos" : "não assistidos";

                var response = new DefaultResponse<Filme>
                {
                    data = listaFilmes,
                    message = listaFilmes.Count > 0 ? $"Filmes {assistidoString} encontrados." : $"Filmes {assistidoString} não foram encontrados.",
                    success = true
                };

               return Ok(response);

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("Listar")]
        public IHttpActionResult Listar()
        {
            try
            {
                FilmesService service = new FilmesService();
                var listaFilmes = service.Listar().OrderBy(x => x.Nome).ThenByDescending(x => x.DtAdicionado).ToList();

                var response = new DefaultResponse<Filme>
                {
                    data = listaFilmes,
                    message = listaFilmes.Count > 0 ? "Filmes encontrados." : "Filmes não foram encontrados.",
                    success = true
                };

                return Ok(response);

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpPost]
        [Route("FilmeAleatorio")]
        public IHttpActionResult FilmeAleatorio([FromBody] List<Categoria> categorias)
        {
            try
            {
                Random rnd = new Random();
                List<Filme> filmesFinais = new List<Filme>();

                if(categorias.Count > 0)
                {
                    CategoriasService service = new CategoriasService();
                    var listaCategoriasFilme = service.ListarPorCategoria(categorias, out string categoriasString);
                    listaCategoriasFilme.Select(x => x.Filmes).ForEach(z => z.ForEach(x => filmesFinais.Add(x)));
                }
                else
                {
                    FilmesService filmesService = new FilmesService();
                    filmesFinais = filmesService.Listar();
                }

                filmesFinais = filmesFinais.Where(filme => !filme.Assistido).ToList();

                if(filmesFinais.Count <= 0)
                {
                    return Ok(new
                    {
                        data = new Filme(),
                        success = false,
                        message = "Não encontramos nenhum filme com os critérios informados."
                    });
                }

                int indiceFilme = rnd.Next(0, filmesFinais.Count - 1);
                var filmeRandom = filmesFinais.ElementAt(indiceFilme);

                var response = new 
                {
                    data = filmeRandom,
                    message = "Filme aleatório selecionado." ,
                    success = true
                };

                return Ok(response);

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpPost]
        [Route("Assistir")]
        public IHttpActionResult Assistir([FromBody] Filme filme)
        {
            try
            {
                FilmesService service = new FilmesService();
                service.AssistirFilme(filme);

                return Ok(new DefaultResponse<Filme>
                {
                    data = new List<Filme>(),
                    message = "Filme marcado como assistido.",
                    success = true
                });

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("Avaliar")]
        public IHttpActionResult Avaliar([FromBody] Filme filme)
        {
            try
            {
                FilmesService service = new FilmesService();
                service.DarNotaFilme(filme.Id, filme.Nota);

                return Ok(new DefaultResponse<Filme>
                {
                    data = new List<Filme>(),
                    message = $"Foi atribuida ao filme {filme.Nome} a nota {filme.Nota}.",
                    success = true
                });

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
