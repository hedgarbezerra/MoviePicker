using KabumCrawling.Domain.Models;
using OurMovies.MoviePicker.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurMovies.MoviePicker.Repository.Repositories
{
    public class DestinarioRepository : BaseRepository<Destinario>
    {
        public DestinarioRepository(ContextoDados contexto = null)
        {
            if (contexto != null)
                _context = contexto;
            else
                _context = new ContextoDados();
        }
        public override Destinario Inserir(Destinario obj)
        {
            obj.DtCadastro = DateTime.Now;
            var objContextual = _context.Destinarios.Add(obj);

            return objContextual;
        }
        public Destinario GetDestinario(int id)
        {
            return _context.Destinarios.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }
    }
}
