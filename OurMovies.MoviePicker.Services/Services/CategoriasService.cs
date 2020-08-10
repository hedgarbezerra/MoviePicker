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
    public class CategoriasService
    {
        private ContextoDados _contexto;
        private CategoriasRepository _repo;
        public CategoriasService()
        {
            _contexto = new ContextoDados();
        }

        public List<Categoria> Listar()
        {
            _repo = new CategoriasRepository(_contexto);

            return _repo.ListarNoTracking().ToList();
        }

        public List<Categoria> Listar(string nomeCategoria = null)
        {
            _repo = new CategoriasRepository(_contexto);

            if(!string.IsNullOrEmpty(nomeCategoria))
                return _repo.ListarNoTracking(x => x.Nome.Contains(nomeCategoria)).ToList();
            else
                return _repo.ListarNoTracking().ToList();
        }

        public void Remover(int id)
        {
            _repo = new CategoriasRepository(_contexto);
            var categoriaCtx = _repo.EncontrarPorId(id);

            _repo.Remover(categoriaCtx);
            _repo.Savechanges();
        }

        public Categoria Cadastrar(Categoria categoria)
        {
            _repo = new CategoriasRepository(_contexto);

            var novaCategoria = _repo.Inserir(categoria);
            _repo.Savechanges();

            return novaCategoria;
        }

        public List<Categoria> ListarPorCategoria(List<Categoria> categorias, out string stringCategorias)
        {
            List<Categoria> filmesDaCategoria = new List<Categoria>();

            _repo = new CategoriasRepository(_contexto);
            stringCategorias = "";

            foreach (var categoria in categorias)
            {
                stringCategorias += categoria.Nome + " ";

                var categoriaCtx = _repo.Listar(x => x.Id == categoria.Id).FirstOrDefault();

                if(categoriaCtx != null && categoriaCtx.Filmes != null)
                    filmesDaCategoria.Add(categoriaCtx);
            }

            return filmesDaCategoria;
        }

        public void Atualizar(string novoNome, int idCategoria)
        {
            _repo = new CategoriasRepository(_contexto);

            var categoriaCtx = _repo.EncontrarPorId(idCategoria);

            categoriaCtx.Nome = novoNome;
            _repo.Atualizar(categoriaCtx);
            _repo.Savechanges();

        }
    }
}
