using KabumCrawling.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurMovies.MoviePicker.Repository.Context.Configuration
{
    public class DestinarioConfiguration : EntityConfiguration<Destinario>
    {
        protected override void ConfigurateFields()
        {
            Property(p => p.Id)
                .HasColumnName("ID")
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            Property(p => p.Nome)
                .HasColumnName("NOME")
                .HasColumnType("VARCHAR")
                .IsRequired();

            Property(p => p.Contato)
                .HasColumnName("CONTATO")
                .HasColumnType("VARCHAR")
                .IsRequired();

            Property(p => p.Email)
                .HasColumnName("EMAIL")
                .HasColumnType("VARCHAR")
                .IsRequired();

            Property(p => p.DtCadastro)
                .HasColumnName("DT_CADASTRO")
                .HasColumnType("datetime");
        }

        protected override void ConfigurateFK()
        {
            HasMany(u => u.Produtos)
          .WithRequired(u => u.Destinario)
          .HasForeignKey(u => u.IdDestinario)
          .WillCascadeOnDelete(false);
        }

        protected override void ConfiguratePK()
        {
            HasKey(x => x.Id);
        }

        protected override void ConfigurateTableName()
        {
            ToTable("TB_DESTINARIOS");
        }
    }
}
