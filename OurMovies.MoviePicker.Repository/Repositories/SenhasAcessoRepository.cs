using OurMovies.MoviePicker.Domain.Models;
using OurMovies.MoviePicker.Repository.Context;
using OurMovies.MoviePicker.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
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

        public bool Login(SenhaAcesso senha, out SenhaAcesso usuarioLogado)
        {
            var usuario = _context.SenhasAcesso.Where(x => x.Usuario == senha.Usuario).FirstOrDefault();

            usuarioLogado = null;

            if (usuario == null)
                return false;

            var logado = Criptografia.VerifyHash(senha.Senha, usuario.Senha);

            if (logado)
                usuarioLogado = usuario;

            return logado;
        }

        public override SenhaAcesso Inserir(SenhaAcesso obj)
        {
            var existeUsuario = _context.SenhasAcesso.Where(x => x.Usuario == obj.Usuario).FirstOrDefault();

            if (existeUsuario != null)
                throw new Exception("Usuário cadastrado previamente.");

            obj.Senha = Criptografia.ComputeHash(obj.Senha);
            obj.DtCriacao = DateTime.Now;

            var objCtx = _context.SenhasAcesso.Add(obj);

            return objCtx;
        }
        public void ResetarSenha(SenhaAcesso usuario, out string novaSenha)
        {
            novaSenha = Criptografia.RandomPassword();

            var usuarioCtx = this.Listar(x => x.Usuario == usuario.Usuario).FirstOrDefault();

            usuarioCtx.Senha = Criptografia.ComputeHash(novaSenha);
        }

    }
}
