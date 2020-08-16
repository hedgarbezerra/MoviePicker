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
            _repo = new FilmesRepository(_contexto);
        }

        public List<Filme> ListarFilmes(string nomeFilme = "")
        {


            var filmes = _repo.Listar(x => x.Nome.Contains(nomeFilme) || x.Categorias.Any(y => y.Nome == nomeFilme)).ToList();

            return filmes;
        }

        public List<Filme> ListarFilmes()
        {


            var filmes = _repo.ListarNoTracking().ToList();

            return filmes;
        }
        public List<Filme> Listar()
        {


            var filmes = _repo.ListarTodos();

            return filmes;
        }
        public List<Filme> ListarFilmes(bool filmeAssistido)
        {


            var filmes = _repo.ListarPorStatus(filmeAssistido); 

            return filmes;
        }

        public Filme GetFilme(Filme filme)
        {


            var filmeEncontrado = _repo.EncontrarPorId(filme.Id);

            return filmeEncontrado;
        }

        public void RemoverFilme(Filme filme)
        {


            _repo.RemoverRelacionamentoSQL(filme);
            _repo.Remover(filme);
            _repo.Savechanges();
        }

        public Filme Cadastrar(Filme filme)
        {


            var existeFilme = _repo.Listar(x => x.Nome.ToLower() == filme.Nome.ToLower()).Any();

            if (existeFilme)
                throw new Exception($"O filme {filme.Nome} já existe. Não é possível cadastrá-lo novamente.");

            var objCtx = _repo.Inserir(filme);
            _repo.Savechanges();

            return objCtx;
        }


        public void DarNotaFilme(int idFilme, int nota)
        {


            _repo.DarNota(idFilme, nota);
            _repo.Savechanges();
        }


        public void AssistirFilme(Filme filme)
        {


            var filmeCtx = _repo.EncontrarPorId(filme.Id);

            _repo.MudarEstadoFilme(filmeCtx, true);
            _repo.Savechanges();
        }
    }
}
