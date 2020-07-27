using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurMovies.MoviePicker.Repository.Interfaces
{
    public interface IEntityConfiguration
    {
        void ConfigurateFK();

        void ConfiguratePK();

        void ConfigurateFields();

        void ConfigurateTableName();
    }
}
