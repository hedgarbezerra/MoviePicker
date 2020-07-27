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

            var objCtx = _context.Filmes.Add(obj);

            return objCtx;
        }
    }
}
