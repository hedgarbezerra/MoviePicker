using OurMovies.MoviePicker.Domain.Models;
using OurMovies.MoviePicker.Repository.Context;
using OurMovies.MoviePicker.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OurMovies.MoviePicker.Repository.Repositories
{
    public class SenhasAcessoRepository : BaseRepository<SenhaAcesso>
    {
        public SenhasAcessoRepository(ContextoDados contexto = null)
        {
            if (contexto != null)
                _context = contexto;
            else
                _context = new ContextoDados();
    }
    public bool Login(SenhaAcesso senha, out SenhaAcesso senhaAutenticada)
        {
            var usuario = _context.SenhasAcesso.Where(x => x.Usuario == senha.Usuario).FirstOrDefault();

            var senhaCripto = Criptografia.ComputeHash(senha.Senha);

            senhaAutenticada = usuario;

            return Criptografia.VerifyHash(senha.Senha, usuario.Senha);
        }

        public override SenhaAcesso Inserir(SenhaAcesso obj)
        {
            var existeUsuario = _context.SenhasAcesso.Where(x => x.Usuario == obj.Usuario).FirstOrDefault();

            if (existeUsuario != null)
                throw new Exception("Usuário cadastrado previamente.");

            obj.Senha = Criptografia.ComputeHash(obj.Senha);
            obj.DtCriacao = DateTime.Now;

            var objCtx = _context.SenhasAcesso.Add(obj);
            Savechanges();

            return objCtx;
        }

    }
}
