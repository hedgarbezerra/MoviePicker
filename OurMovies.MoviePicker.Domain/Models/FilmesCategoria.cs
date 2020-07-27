using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurMovies.MoviePicker.Domain.Models
{
    public class FilmesCategoria
    {
        public int IdFilme { get; set; }
        public int IdCategoria { get; set; }
        public virtual Filme Filme { get; set; }
        public virtual Categoria Categoria { get; set; }
    }
}
