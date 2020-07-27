using OurMovies.MoviePicker.Domain.Models;
using OurMovies.MoviePicker.Repository.Context.Configuration;
using System.Data.Entity;
using System.Linq;

namespace OurMovies.MoviePicker.Repository.Context
{
    public class ContextoDados : DbContext
    {
        public ContextoDados()
            : base("name=DbFilmes")
        {
        }
        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Configurations.Add(new SenhaConfiguration());
            builder.Configurations.Add(new FilmesConfiguration());
            builder.Configurations.Add(new CategoriasConfiguration());
        }
        public virtual DbSet<SenhaAcesso> SenhasAcesso { get; set; }
        public virtual DbSet<Filme> Filmes { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }
    }
}
