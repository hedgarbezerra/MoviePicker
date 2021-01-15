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
            this._context = contexto != null ? contexto : new ContextoDados();
        }
        public override Categoria Inserir(Categoria obj)
        {
            obj.DtAdicionado = DateTime.Now;
            var objCtx = _context.Categorias.Add(obj);

            return objCtx;
        }
        public void RemoverRelacionamentoSQL(Categoria categoria)
        {
            string sqlQuery = $@"DELETE FROM TB_FILMES_CATEGORIA WHERE REF_ID_CATEGORIA = {categoria.Id}";

            _context.Database.ExecuteSqlCommand(System.Data.Entity.TransactionalBehavior.EnsureTransaction, sqlQuery);

        }
    }
}
