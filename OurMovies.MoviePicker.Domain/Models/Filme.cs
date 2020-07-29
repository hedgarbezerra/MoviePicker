using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurMovies.MoviePicker.Domain.Models
{
    public class Filme
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Nota { get; set; }
        public bool Assistido { get; set; }
        public DateTime DtAdicionado { get; set; }
        public virtual ICollection<Categoria> Categorias { get; set; }
    }
}
