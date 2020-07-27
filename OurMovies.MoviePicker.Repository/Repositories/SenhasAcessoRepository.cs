using OurMovies.MoviePicker.Domain.Models;
using OurMovies.MoviePicker.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OurMovies.MoviePicker.Repository.Repositories
{
    public class SenhasAcessoRepository : BaseRepository<SenhaAcesso>
    {
        public bool Login(SenhaAcesso senha, out SenhaAcesso senhaAutenticada)
        {
            var senhaCripto = Criptografia.ComputeHash(senha.Senha);

            var senhaEncontrada = _context.SenhasAcesso.AsNoTracking().Where(x=> x.Senha == senhaCripto).FirstOrDefault();
            senhaAutenticada = senhaEncontrada;

            return senhaEncontrada != null;
        }

        public override SenhaAcesso Inserir(SenhaAcesso obj)
        {
            obj.Senha = Criptografia.ComputeHash(obj.Senha);
            obj.DtCriacao = DateTime.Now;

            var objCtx = base.Inserir(obj);
            Savechanges();

            return objCtx;
        }

    }
}
