using KabumCrawling.Domain.Models;
using OurMovies.MoviePicker.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurMovies.MoviePicker.Repository.Repositories
{
    public class NotificacaoRepository : BaseRepository<NotificacaoProduto>
    {
        public NotificacaoRepository(ContextoDados contexto = null)
        {
            if (contexto != null)
                _context = contexto;
            else
                _context = new ContextoDados();

        }
        public override NotificacaoProduto Inserir(NotificacaoProduto obj)
        {
            obj.DtCadastro = DateTime.Now;
            var objContextual = _context.NotificacaoProdutos.Add(obj);

            return objContextual;
        }
    }
}
