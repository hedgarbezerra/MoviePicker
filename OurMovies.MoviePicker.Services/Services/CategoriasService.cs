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
            _repo = new CategoriasRepository(_contexto);
        }

        public List<Categoria> Listar()
        {
            

            return _repo.ListarNoTracking().ToList();
        }

        public List<Categoria> Listar(string nomeCategoria = null)
        {
            

            if(!string.IsNullOrEmpty(nomeCategoria))
                return _repo.ListarNoTracking(x => x.Nome.Contains(nomeCategoria)).ToList();
            else
                return _repo.ListarNoTracking().ToList();
        }

        public void Remover(Categoria categoria)
        {
            
            _repo.RemoverRelacionamentoSQL(categoria);
            _repo.Remover(categoria);

            _repo.Savechanges();
        }

        public Categoria Cadastrar(Categoria categoria)
        {
            

            var categoriaExiste = _repo.Listar(x => x.Nome.ToLower() == categoria.Nome.ToLower()).Any();

            if (categoriaExiste)
                throw new Exception($"A categoria {categoria.Nome} já está cadastrada.");

            var novaCategoria = _repo.Inserir(categoria);
            _repo.Savechanges();

            return novaCategoria;
        }

        public List<Categoria> ListarPorCategoria(List<Categoria> categorias, out string stringCategorias)
        {
            List<Categoria> filmesDaCategoria = new List<Categoria>();

            
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
            

            var categoriaCtx = _repo.EncontrarPorId(idCategoria);

            categoriaCtx.Nome = novoNome;
            _repo.Atualizar(categoriaCtx);
            _repo.Savechanges();

        }
    }
}
