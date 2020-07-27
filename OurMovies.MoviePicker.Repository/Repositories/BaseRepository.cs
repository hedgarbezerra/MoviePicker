using OurMovies.MoviePicker.Repository.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OurMovies.MoviePicker.Repository.Repositories
{
    public abstract class BaseRepository<T>
        where T : class
    {
        protected ContextoDados _context;

        public virtual T Inserir(T obj)
        {
            var objContextual = _context.Set<T>().Add(obj);

            return objContextual;
        }
        public virtual void Remover(T obj)
        {
            _context.Entry(obj).State = EntityState.Deleted;
            _context.Set<T>().Remove(obj);
        }
        public virtual void Atualizar(T obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
        }
        public virtual T EncontrarPorId(int id)
        {
            var objContextual = _context.Set<T>().Find(id);
            return objContextual;
        }
        public virtual IEnumerable<T> Listar()
        {
            return _context.Set<T>().AsParallel();
        }
        public virtual IEnumerable<T> ListarNoTracking()
        {
            return _context.Set<T>().AsNoTracking();
        }
        public virtual IEnumerable<T> Listar(Expression<Func<T, bool>> expression = null)
        {
            var dados = _context.Set<T>().AsQueryable();

            if (expression != null)
                dados = dados.Where(expression);

            return dados;
        }
        public virtual IEnumerable<T> Listar(Expression<Func<T, bool>> expression = null, Expression<Func<T, object>> order = null, int? count = 0, int? skip = 0, bool reverse = false)
        {
            var dados = _context.Set<T>().AsQueryable();

            if (expression != null)
                dados = dados.Where(expression);

            if (order != null)
                dados = reverse ? dados.OrderByDescending(order) : dados.OrderBy(order);

            if (count > 0 && skip > 0)
                dados = dados.Skip((int)skip).Take((int)count);

            return dados.AsEnumerable();
        }

        public void Savechanges()
        {
            this._context.SaveChanges();
        }
        public void Dispose()
        {
            this._context.Dispose();
        }
        public ContextoDados RetornaContexto()
        {
            return this._context;
        }
    }
}
