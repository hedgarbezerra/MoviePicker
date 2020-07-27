using OurMovies.MoviePicker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurMovies.MoviePicker.Repository.Context.Configuration
{
    public class FilmesConfiguration : EntityConfiguration<Filme>
    {
        protected override void ConfigurateFields()
        {
            Property(x => x.Id)
                .HasColumnName("ID")
                .HasColumnType("int")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            Property(x => x.Nome)
                .HasColumnName("NOME")
                .HasColumnType("varchar")
                .HasMaxLength(255)
                .IsRequired();

            Property(x => x.Descricao)
                .HasColumnName("DESCRICAO")
                .HasColumnType("varchar")
                .IsOptional();

            Property(x => x.DtAdicionado)
                .HasColumnName("DT_ADICIONADO")
                .HasColumnType("datetime")
                .IsOptional();
        }

        protected override void ConfigurateFK()
        {
            HasMany(x => x.Categorias)
                .WithMany(x => x.Filmes)
                .Map(x =>
                {
                    x.MapLeftKey("REF_ID_FILME");
                    x.MapRightKey("REF_ID_CATEGORIA");
                    x.ToTable("TB_FILMES_CATEGORIA");
                    
                });
        }

        protected override void ConfiguratePK()
        {
            HasKey(x => x.Id);
        }

        protected override void ConfigurateTableName()
        {
            ToTable("TB_FILMES");
        }
    }
}
