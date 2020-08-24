using MailKit.Net.Smtp;
using MimeKit;
using OurMovies.MoviePicker.Domain.DTO;
using OurMovies.MoviePicker.Domain.Models;
using System;
using System.Collections.Generic;

namespace OurMovies.MoviePicker.Services.Notification
{
    public class EmailNotification : NotificationBase
    {
        private MimeKit.Text.TextFormat defaultFormat;
        public EmailNotification()
        {
            this.defaultFormat = MimeKit.Text.TextFormat.Text;
        }
        public EmailNotification(MimeKit.Text.TextFormat tipoEmail = MimeKit.Text.TextFormat.Text)
        {
            this.defaultFormat = tipoEmail;
        }
        public override void Notificar(List<DTOContato> listaContato, string titulo, string mensagem)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Nossos Filmes", _email));
            message.Subject = titulo;

            listaContato.ForEach(x => message.To.Add(new MailboxAddress(x.Nome, x.Contato)));

            message.Body = new TextPart(this.defaultFormat)
            {
                Text = mensagem
            };

           EnviarMensagem(message);
        }

        public override void Notificar(DTOContato contato, string titulo, string mensagem)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Nossos Filmes", _email));
            message.To.Add(new MailboxAddress(contato.Nome, contato.Contato));
            message.Subject = titulo;

            message.Body = new TextPart(this.defaultFormat)
            {
                Text = mensagem
            };

            EnviarMensagem(message);
        }

        private void EnviarMensagem(MimeMessage mensagem)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    client.Connect(_smtp, _smtpPort, true);

                    client.Authenticate(_email, _emailPassword);

                    client.Send(mensagem);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Houve um erro ao enviar este e-mail.", ex);
            }

        }

        private string MontaEmail()
        {
            string htmlEmail = $@"";

            return htmlEmail;
        }
    }
}
