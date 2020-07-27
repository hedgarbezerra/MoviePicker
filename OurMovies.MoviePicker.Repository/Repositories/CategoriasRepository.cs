using OurMovies.MoviePicker.Domain.Models;
using OurMovies.MoviePicker.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurMovies.MoviePicker.Repository.Repositories
{
    public class CategoriasRepository : BaseRepository<Categoria>
    {
        public CategoriasRepository(ContextoDados contexto = null)
        {
            if (contexto != null)
                _context = contexto;
            else
                _context = new ContextoDados();
        }
        public override Categoria Inserir(Categoria obj)
        {
            obj.DtAdicionado = DateTime.Now;
            var objCtx = _context.Categorias.Add(obj);

            return objCtx;
        }
    }
}
