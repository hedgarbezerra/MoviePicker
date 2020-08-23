﻿using OurMovies.MoviePicker.Domain.DTO;
using OurMovies.MoviePicker.Domain.Models;
using OurMovies.MoviePicker.Services.Notification;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace KabumCrawling.Services.Notification
{

    //public class NotificationBase : INotification
    //{
    //    protected string _smtp = ConfigurationManager.AppSettings["smtp"];
    //    protected int _smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]);
    //    protected string _email = ConfigurationManager.AppSettings["email"];
    //    protected string _emailPassword = ConfigurationManager.AppSettings["senhaEmail"];

    //    public virtual void Notificar(List<DTOContato> listaContato, string titulo, string mensagem)
    //    {
    //        throw new NotImplementedException();
    //    }
    //    public virtual void Notificar(DTOContato contato, string titulo, string mensagem)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    public abstract class NotificationBase : INotification
    {
        protected string _smtp = ConfigurationManager.AppSettings["smtp"];
        protected int _smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]);
        protected string _email = ConfigurationManager.AppSettings["email"];
        protected string _emailPassword = ConfigurationManager.AppSettings["senhaEmail"];

        public abstract void Notificar(List<DTOContato> listaContato, string titulo, string mensagem);
        public abstract void Notificar(DTOContato contato, string titulo, string mensagem);
    }
}
