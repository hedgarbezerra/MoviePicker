using OurMovies.MoviePicker.Domain.Models;
using OurMovies.MoviePicker.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurMovies.MoviePicker.Repository.Repositories
{
    public class FilmesRepository: BaseRepository<Filme>
    {
        public FilmesRepository(ContextoDados contexto = null)
        {
            if (contexto != null)
                _context = contexto;
            else
                _context = new ContextoDados();
        }
        public override Filme Inserir(Filme obj)
        {
            obj.DtAdicionado = DateTime.Now;
            obj.Assistido = false;

            List<Categoria> categorias = new List<Categoria>();

            foreach (var item in obj.Categorias.ToList())
            {
                var categoriaCtx = _context.Categorias.Find(item.Id);
                categorias.Add(categoriaCtx);
            } 

            obj.Categorias = categorias;
            var objCtx = _context.Filmes.Add(obj);

            return objCtx;
        }

        public void MudarEstadoFilme(Filme filme, bool estado = false)
        {
            var filmeCtx = _context.Filmes.Find(filme.Id);

            if (filmeCtx !=null)
            {
                filmeCtx.Assistido = estado;
            }
        }
        public void DarNota(int idFilme, int nota)
        {
            var filmeCtx = _context.Filmes.Find(idFilme);

            if (filmeCtx != null)
            {
                filmeCtx.Assistido = true;
                filmeCtx.Nota = nota;
            }
        }
        public List<Filme> ListarTodos()
        {
            _context.Configuration.ProxyCreationEnabled = false;
            var filmes = _context.Filmes.Include("Categorias").AsNoTracking();

            return filmes.ToList();
        }

        public List<Filme> ListarPorStatus(bool assistido = false)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            var filmes = _context.Filmes.Include("Categorias").AsNoTracking().Where(x => x.Assistido == assistido);

            return filmes.ToList();
        }
    }
}
