using OurMovies.MoviePicker.Domain.DTO;
using OurMovies.MoviePicker.Domain.Models;
using OurMovies.MoviePicker.Repository.Context;
using OurMovies.MoviePicker.Repository.Repositories;
using OurMovies.MoviePicker.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurMovies.MoviePicker.Services.Services
{
    public class AutenticacaoService
    {
        private ContextoDados _contexto;
        private SenhasAcessoRepository repo;
        public AutenticacaoService()
        {
            _contexto = new ContextoDados();
            repo = new SenhasAcessoRepository(_contexto);
        }

        public bool Autenticar(DTOUsuario usuario, out SenhaAcesso usuarioLogado)
        {

            return repo.Login(new SenhaAcesso
            {
                Usuario = usuario.Usuario,
                Senha = usuario.Senha
            }, out usuarioLogado);

        }

        public SenhaAcesso Cadastrar(DTOUsuario usuario)
        {
            var usuarioCtx = repo.Inserir(new SenhaAcesso { Usuario = usuario.Usuario, Senha = usuario.Senha });

            repo.Savechanges();

            return usuarioCtx;
        }
        public List<SenhaAcesso> Listar(string usuario)
        {
            return repo.Listar(x => x.Usuario == usuario).ToList();
        }

        public List<SenhaAcesso> Listar()
        {
            return repo.Listar().ToList();
        }
        public SenhaAcesso GetUsuario(string usuario)
        {
            return repo.Listar(x => x.Usuario == usuario).FirstOrDefault();
        }

        public void ResetarSenhaUsuario(DTOUsuario usuario, out string novaSenhaSaida)
        {
            var usuarioCtx = GetUsuario(usuario.Usuario);

            if (usuarioCtx == null)
            {
                throw new Exception("Usuário não encontrado");
            }

            repo.ResetarSenha(usuarioCtx, out novaSenhaSaida);

            repo.Savechanges();
        }
        public void AtualizarSenha(DTOUsuario usuario)
        {
            var usuarioCtx = this.GetUsuario(usuario.Usuario);

            usuarioCtx.Senha = Hashing.ComputeHash(usuario.Senha);

            repo.Atualizar(usuarioCtx);
            repo.Savechanges();
        }
    }
}
