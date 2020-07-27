using OurMovies.MoviePicker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurMovies.MoviePicker.Repository.Context.Configuration
{
    public class SenhaConfiguration : EntityConfiguration<SenhaAcesso>
    {
        protected override void ConfigurateFields()
        {
            Property(x => x.Id)
                .HasColumnName("ID")
                .HasColumnType("int")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            Property(x => x.Usuario)
               .HasColumnName("USUARIO")
               .HasMaxLength(255)
               .IsRequired()
               .HasColumnType("varchar");

            Property(x => x.Senha)
                .HasColumnName("SENHA")
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnType("varchar");

            Property(x => x.DtCriacao)
                .HasColumnName("DT_CRIACAO")
                .HasColumnType("datetime");
        }

        protected override void ConfigurateFK()
        {
        }

        protected override void ConfiguratePK()
        {
            HasKey(x => x.Id);
        }

        protected override void ConfigurateTableName()
        {
            ToTable("TB_SENHASACESSO");
        }
    }
}
