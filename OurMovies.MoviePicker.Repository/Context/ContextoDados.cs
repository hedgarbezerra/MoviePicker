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
        }
        public virtual DbSet<SenhaAcesso> SenhasAcesso { get; set; }
    }
}
