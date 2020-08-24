using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurMovies.MoviePicker.Domain.DTO
{
    public class DTOUsuario
    {
        [MinLength(6)]
        public string Usuario { get; set; }

        [MinLength(8)]
        public string Senha { get; set; }
    }
}
