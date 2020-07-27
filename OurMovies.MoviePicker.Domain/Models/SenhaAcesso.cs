using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurMovies.MoviePicker.Domain.Models
{
    public class SenhaAcesso
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public DateTime DtCriacao { get; set; }

    }
}
