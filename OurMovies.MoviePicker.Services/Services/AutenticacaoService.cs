using OurMovies.MoviePicker.Domain.DTO;
using OurMovies.MoviePicker.Domain.Models;
using OurMovies.MoviePicker.Repository.Context;
using OurMovies.MoviePicker.Repository.Repositories;
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
        }

        public bool Autenticar(DTOUsuario usuario, out SenhaAcesso usuarioLogado)
        {
            repo = new SenhasAcessoRepository(_contexto);

            return repo.Login(new SenhaAcesso
            {
                Usuario = usuario.Usuario,
                Senha = usuario.Senha
            }, out usuarioLogado);

        }

        public SenhaAcesso Cadastrar(DTOUsuario usuario)
        {            
            repo = new SenhasAcessoRepository(_contexto);

            return repo.Inserir(new SenhaAcesso { Usuario = usuario.Usuario, Senha = usuario.Senha });
        }
        public List<SenhaAcesso> Listar(string usuario)
        {
            repo = new SenhasAcessoRepository(_contexto);

            return repo.Listar(x => x.Usuario == usuario).ToList();
        }

        public List<SenhaAcesso> Listar()
        {
            repo = new SenhasAcessoRepository(_contexto);

            return repo.Listar().ToList();
        }

    }
}
