using KabumCrawling.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurMovies.MoviePicker.Repository.Context.Configuration
{
    public class NotificacaoConfiguration : EntityConfiguration<NotificacaoProduto>
    {
        protected override void ConfigurateFields()
        {
            Property(p => p.Id)
                .HasColumnName("ID")
                .HasColumnType("INT")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            Property(p => p.NomeProduto)
                .HasColumnName("NOME_PRODUTO")
                .HasColumnType("VARCHAR")
                .IsRequired();

            Property(p => p.ValorMinProduto)
                .HasColumnName("VALOR_MIN")
                .HasColumnType("decimal")
                .HasPrecision(14, 2)
                .IsOptional();

            Property(p => p.IdDestinario)
               .HasColumnName("ID_DESTINARIO")
               .HasColumnType("int")
               .IsRequired();


            Property(p => p.ValorMaxProduto)
                .HasColumnName("VALOR_MAX")
                .HasColumnType("decimal")
                .HasPrecision(14, 2)
                .IsOptional();

            Property(p => p.DtCadastro)
                .HasColumnName("DT_CADASTRO")
                .HasColumnType("datetime");
        }

        protected override void ConfigurateFK()
        {

        }

        protected override void ConfiguratePK()
        {
            HasKey(a => a.Id);
        }

        protected override void ConfigurateTableName()
        {
            ToTable("TB_NOTIFICAR_PRODUTO");
        }
    }
}
