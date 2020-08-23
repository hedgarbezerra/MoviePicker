using OurMovies.MoviePicker.Domain.DTO;
using OurMovies.MoviePicker.Domain.Models;
using System.Collections.Generic;

namespace OurMovies.MoviePicker.Services.Notification
{
    public interface INotification
    {
        void Notificar(List<DTOContato> listaContato, string titulo, string mensagem);
        void Notificar(DTOContato contato, string titulo, string mensagem);
    }
}
