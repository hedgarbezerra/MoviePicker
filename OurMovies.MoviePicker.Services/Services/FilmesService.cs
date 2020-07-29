using OurMovies.MoviePicker.Domain.Models;
using OurMovies.MoviePicker.Repository.Context;
using OurMovies.MoviePicker.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurMovies.MoviePicker.Services.Services
{
    public class FilmesService
    {
        private ContextoDados _contexto;
        private FilmesRepository _repo;
        public FilmesService()
        {
            this._contexto = new ContextoDados();
        }

        public List<Filme> ListarFilmes(string nomeFilme = "")
        {
            _repo = new FilmesRepository(_contexto);

            var filmes = _repo.Listar(x => x.Nome.Contains(nomeFilme) || x.Categorias.Any(y => y.Nome == nomeFilme)).ToList();

            return filmes;
        }

        public List<Filme> ListarFilmes()
        {
            _repo = new FilmesRepository(_contexto);

            var filmes = _repo.ListarNoTracking().ToList();

            return filmes;
        }
        public List<Filme> Listar()
        {
            _repo = new FilmesRepository(_contexto);

            var filmes = _repo.ListarTodos();

            return filmes;
        }
        public List<Filme> ListarFilmes(bool filmeAssistido)
        {
            _repo = new FilmesRepository(_contexto);

            var filmes = _repo.ListarPorStatus(filmeAssistido); //_repo.Listar(x => x.Assistido == filmeAssistido).ToList();

            return filmes;
        }

        public Filme Cadastrar(Filme filme)
        {
            _repo = new FilmesRepository(_contexto);

            var objCtx = _repo.Inserir(filme);
            _repo.Savechanges();

            return objCtx;
        }


        public void AssistirFilme(Filme filme)
        {            
            _repo = new FilmesRepository(_contexto);

            var filmeCtx = _repo.EncontrarPorId(filme.Id);

            _repo.MudarEstadoFilme(filmeCtx, true);
            _repo.Savechanges();
        }
    }
}
